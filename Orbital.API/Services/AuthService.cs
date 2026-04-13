using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;
using Orbital.API.DTOs;
using Orbital.API.Models;

namespace Orbital.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<object?> Login(UsuarioLoginDto dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u =>
                    u.Correo == dto.Correo &&
                    u.Contrasena_Hash == dto.Password &&
                    u.Activo == true);

            if (usuario == null)
                return null;

            return new
            {
                usuario.Id_Usuario,
                usuario.Nombre,
                usuario.Correo,
                usuario.Id_Rol
            };
        }

        // 🔥 AQUÍ VA TU CÓDIGO DE REGISTRO
        public async Task<object> Register(UsuarioCreateDto dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Contrasena_Hash = dto.Password, // ✔ aquí va la conversión
                Id_Rol = dto.Id_Rol,
                Id_Jerarquia = dto.Id_Jerarquia,
                Activo = true,
                Fecha_Registro = DateTime.Now
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new
            {
                usuario.Id_Usuario,
                usuario.Nombre,
                usuario.Correo
            };
        }
    }
}