using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;
using Orbital.API.DTOs;
using Orbital.API.Models;

namespace Orbital.API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioResponseDto>> GetUsuarios()
        {
            return await _context.Usuarios
                .Include(x => x.Rol)
                .Include(x => x.Jerarquia)
                .Select(x => new UsuarioResponseDto
                {
                    Id_Usuario = x.Id_Usuario,
                    Nombre = x.Nombre,
                    Correo = x.Correo,
                    Rol = x.Rol.Nombre_Rol,
                    Jerarquia = x.Jerarquia.Nombre_Jerarquia,
                    Activo = x.Activo
                })
                .ToListAsync();
        }

        public async Task<UsuarioResponseDto?> GetUsuarioById(int id)
        {
            return await _context.Usuarios
                .Include(x => x.Rol)
                .Include(x => x.Jerarquia)
                .Where(x => x.Id_Usuario == id)
                .Select(x => new UsuarioResponseDto
                {
                    Id_Usuario = x.Id_Usuario,
                    Nombre = x.Nombre,
                    Correo = x.Correo,
                    Rol = x.Rol.Nombre_Rol,
                    Jerarquia = x.Jerarquia.Nombre_Jerarquia,
                    Activo = x.Activo
                })
                .FirstOrDefaultAsync();
        }

        public async Task UpdateUsuario(int id, UsuarioUpdateDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return;

            if (dto.Nombre != null)
                usuario.Nombre = dto.Nombre;

            if (dto.Correo != null)
                usuario.Correo = dto.Correo;

            if (dto.Contrasena != null)
                usuario.Contrasena_Hash = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena);

            if (dto.IdRol != null)
                usuario.Id_Rol = dto.IdRol.Value;

            if (dto.Activo != null)
                usuario.Activo = dto.Activo.Value;

            if (dto.IdJerarquia != null)
                usuario.Id_Jerarquia = dto.IdJerarquia.Value;

            // Nivel de poder y equipo viven en miembro_equipo
            if (dto.NivelPoder != null || dto.IdEquipo != null)
            {
                var miembro = await _context.MiembrosEquipo
                    .Where(m => m.Id_Usuario == id && m.Activo)
                    .FirstOrDefaultAsync();

                if (dto.IdEquipo != null && (miembro == null || miembro.Id_Equipo != dto.IdEquipo.Value))
                {
                    // Desactivar membresía actual si existe
                    if (miembro != null)
                        miembro.Activo = false;

                    // Crear nueva membresía en el equipo nuevo
                    var nuevaMembresia = new MiembroEquipo
                    {
                        Id_Equipo = dto.IdEquipo.Value,
                        Id_Usuario = id,
                        Nivel_Poder = dto.NivelPoder ?? miembro?.Nivel_Poder ?? 0,
                        Rol_Equipo = "Combate",
                        Fecha_Ingreso = DateTime.UtcNow,
                        Activo = true
                    };
                    _context.MiembrosEquipo.Add(nuevaMembresia);
                }
                else if (miembro != null && dto.NivelPoder != null)
                {
                    // Solo actualizar nivel_poder en la membresía actual
                    miembro.Nivel_Poder = dto.NivelPoder.Value;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<UsuarioResponseDto>> ListarUsuarios(
            string? nombre, bool? activo,
            DateTime? fechaDesde, DateTime? fechaHasta,
            int? jerarquiaId, string? letra,
            int? nivelPoderMin, int? nivelPoderMax,
            string? ordenarPor, bool desc)
        {
            var query = from u in _context.Usuarios
                        join r in _context.Roles on u.Id_Rol equals r.Id_Rol
                        join j in _context.Jerarquias on u.Id_Jerarquia equals j.Id_Jerarquia
                        join me in _context.MiembrosEquipo.Where(m => m.Activo)
                            on u.Id_Usuario equals me.Id_Usuario into miembros
                        from miembro in miembros.DefaultIfEmpty()
                        select new
                        {
                            u.Id_Usuario,
                            u.Nombre,
                            u.Correo,
                            NombreRol = r.Nombre_Rol,
                            NombreJerarquia = j.Nombre_Jerarquia,
                            u.Activo,
                            u.Fecha_Registro,
                            u.Id_Jerarquia,
                            NivelPoder = (int?)miembro.Nivel_Poder,
                            j.Nivel_Poder_Minimo
                        };

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(x => x.Nombre.Contains(nombre));

            if (activo.HasValue)
                query = query.Where(x => x.Activo == activo.Value);

            if (fechaDesde.HasValue)
                query = query.Where(x => x.Fecha_Registro >= fechaDesde.Value);

            if (fechaHasta.HasValue)
                query = query.Where(x => x.Fecha_Registro <= fechaHasta.Value);

            if (jerarquiaId.HasValue)
                query = query.Where(x => x.Id_Jerarquia == jerarquiaId.Value);

            if (!string.IsNullOrEmpty(letra))
                query = query.Where(x => x.Nombre.StartsWith(letra));

            if (nivelPoderMin.HasValue)
                query = query.Where(x => x.NivelPoder >= nivelPoderMin.Value);

            if (nivelPoderMax.HasValue)
                query = query.Where(x => x.NivelPoder <= nivelPoderMax.Value);

            var ordenada = (ordenarPor?.ToLower(), desc) switch
            {
                ("fecha", false)       => query.OrderBy(x => x.Fecha_Registro),
                ("fecha", true)        => query.OrderByDescending(x => x.Fecha_Registro),
                ("nivel_poder", false) => query.OrderBy(x => x.NivelPoder),
                ("nivel_poder", true)  => query.OrderByDescending(x => x.NivelPoder),
                ("jerarquia", false)   => query.OrderBy(x => x.Nivel_Poder_Minimo),
                ("jerarquia", true)    => query.OrderByDescending(x => x.Nivel_Poder_Minimo),
                (_, false)             => query.OrderBy(x => x.Nombre),
                (_, true)              => query.OrderByDescending(x => x.Nombre),
            };

            var resultado = await ordenada.ToListAsync();

            return resultado.Select(x => new UsuarioResponseDto
            {
                Id_Usuario = x.Id_Usuario,
                Nombre = x.Nombre,
                Correo = x.Correo,
                Rol = x.NombreRol,
                Jerarquia = x.NombreJerarquia,
                Activo = x.Activo,
                Fecha_Registro = x.Fecha_Registro,
                Nivel_Poder = x.NivelPoder
            }).ToList();
        }

        public async Task<Dictionary<string, List<UsuarioResponseDto>>> ObtenerUltimos3UsuariosPorRol()
        {
            var usuarios = await _context.Usuarios
                .Include(x => x.Rol)
                .Include(x => x.Jerarquia)
                .OrderByDescending(x => x.Fecha_Registro)
                .Select(x => new UsuarioResponseDto
                {
                    Id_Usuario = x.Id_Usuario,
                    Nombre = x.Nombre,
                    Correo = x.Correo,
                    Rol = x.Rol.Nombre_Rol,
                    Jerarquia = x.Jerarquia.Nombre_Jerarquia,
                    Activo = x.Activo
                })
                .ToListAsync();

            var resultado = usuarios
                .GroupBy(u => u.Rol)
                .ToDictionary(
                    g => g.Key,
                    g => g.Take(3).ToList()
                );

            return resultado;
        }
    }
}