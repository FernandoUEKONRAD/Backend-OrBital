namespace Orbital.API.DTOs
{
    public class UsuarioResponseDto
    {
        public int Id_Usuario { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Rol { get; set; }

        public string Jerarquia { get; set; }

        public bool Activo { get; set; }
    }
}