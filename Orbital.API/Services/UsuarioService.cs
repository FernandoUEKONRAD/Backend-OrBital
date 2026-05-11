using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;
using Orbital.API.DTOs;

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
            var usuarios = await _context.Usuarios
                .Include(x => x.Rol)
                .Include(x => x.Jerarquia)
                .Select(x => new UsuarioResponseDto
                {
                    Id_Usuario = x.Id_Usuario,
                    Nombre = x.Nombre,
                    Correo = x.Correo,
                    Rol = x.Rol.Nombre_Rol,
                    Jerarquia = x.Jerarquia.Nombre_Jerarquia, // ← CORREGIDO
                    Activo = x.Activo
                })
                .ToListAsync();

            return usuarios;
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