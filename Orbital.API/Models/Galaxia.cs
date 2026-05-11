using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("galaxia")]
    public class Galaxia
    {
        [Key]
        [Column("id_galaxia")]
        public int Id_Galaxia { get; set; }

        [Column("nombre")]
        [Required]
        public string Nombre { get; set; } = null!;

        [Column("descripcion")]
        public string? Descripcion { get; set; }
    }
}
