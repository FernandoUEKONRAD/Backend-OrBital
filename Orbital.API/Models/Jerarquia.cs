using System.ComponentModel.DataAnnotations;

namespace Orbital.API.Models
{
    public class Jerarquia
    {
        [Key]
        public int Id_Jerarquia { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public bool Activo { get; set; }
    }
}