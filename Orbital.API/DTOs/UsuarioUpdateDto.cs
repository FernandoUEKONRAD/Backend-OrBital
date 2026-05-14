namespace Orbital.API.DTOs
{
    public class UsuarioUpdateDto
    {
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
        public int? IdRol { get; set; }
        public bool? Activo { get; set; }
        public int? IdJerarquia { get; set; }
        public int? NivelPoder { get; set; }
        public int? IdEquipo { get; set; }         // mueve al usuario de equipo
    }
}