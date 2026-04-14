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
                    Jerarquia = x.Jerarquia.Nombre,
                    Activo = x.Activo
                })
                .ToListAsync();

            return usuarios;
        }
    }
}