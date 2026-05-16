namespace Orbital.API.Models
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Contrasena_Hash { get; set; } = string.Empty;

        public int Id_Rol { get; set; }

        public int Id_Jerarquia { get; set; }

        public bool Activo { get; set; }

        public DateTime Fecha_Registro { get; set; }

        public DateTime? Ultimo_Acceso { get; set; }

        public Rol Rol { get; set; } = null!;

        public Jerarquia Jerarquia { get; set; } = null!;
    }
}