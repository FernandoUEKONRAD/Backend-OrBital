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