namespace Orbital.API.DTOs
{
    public class UsuarioLoginDto
    {
        public string Correo { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}