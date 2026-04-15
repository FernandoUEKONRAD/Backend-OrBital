using Orbital.API.DTOs;
using Orbital.API.Models;
using Orbital.API.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace Orbital.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _repo;

        public AuthService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> Login(UsuarioLoginDto dto)
        {
            var usuario = await _repo.ObtenerPorEmail(dto.Correo.ToLower());

            if (usuario == null)
                return "Usuario no encontrado";

            var hash = HashPassword(dto.Password);

            if (usuario.Contrasena_Hash != hash)
                return "Password incorrecta";

            return "Login correcto";
        }

        public async Task<Usuario> Register(UsuarioCreateDto dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo.ToLower(), // 🔥 IMPORTANTE
                Contrasena_Hash = HashPassword(dto.Password),
                Id_Rol = dto.Id_Rol,
                Id_Jerarquia = dto.Id_Jerarquia,
                Activo = true,
                Fecha_Registro = DateTime.Now
            };

            await _repo.Crear(usuario);

            return usuario;
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            return Convert.ToBase64String(bytes);
        }
    }
}