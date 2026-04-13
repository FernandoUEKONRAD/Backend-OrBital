using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int Id_Usuario { get; set; }

        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Contrasena_Hash { get; set; } = null!;

        public int Id_Rol { get; set; }

        public int Id_Jerarquia { get; set; }

        public bool Activo { get; set; }

        public DateTime Fecha_Registro { get; set; }

        public DateTime? Ultimo_Acceso { get; set; }

        public Rol Rol { get; set; } = null!;

        public Jerarquia Jerarquia { get; set; } = null!;
    }
}