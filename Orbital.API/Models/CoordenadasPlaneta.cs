using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("coordenada_planeta")]
    public class CoordenadasPlaneta
    {
        [Key]
        [Column("id_coordenada")]
        public int Id_Coordenada { get; set; }

        [Column("id_planeta")]
        [Required]
        public int Id_Planeta { get; set; }

        [Column("coordenada_x")]
        public decimal Coordenada_X { get; set; }

        [Column("coordenada_y")]
        public decimal Coordenada_Y { get; set; }

        [Column("coordenada_z")]
        public decimal Coordenada_Z { get; set; }

        [ForeignKey("Id_Planeta")]
        public Planeta? Planeta { get; set; }
    }
}
