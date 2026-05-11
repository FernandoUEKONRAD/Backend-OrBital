using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("tipo_atmosfera")]
    public class TipoAtmosfera
    {
        [Key]
        [Column("id_atmosfera")]
        public int Id_Atmosfera { get; set; }

        [Column("nombre")]
        [Required]
        public string Nombre { get; set; } = null!;

        [Column("descripcion")]
        public string? Descripcion { get; set; }
    }
}
