namespace Orbital.API.DTOs
{
    public class UsuarioCreateDto
    {
        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int Id_Rol { get; set; }

        public int Id_Jerarquia { get; set; }
    }
}