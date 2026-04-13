namespace Orbital.API.DTOs
{
    public class UsuarioCreateDto
    {
        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Password { get; set; }

        public int Id_Rol { get; set; }

        public int Id_Jerarquia { get; set; }
    }
}