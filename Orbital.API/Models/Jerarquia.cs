using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("jerarquia")]
    public class Jerarquia
    {
        [Key]
        [Column("id_jerarquia")]
        public int Id_Jerarquia { get; set; }

        [Column("nombre_jerarquia")]
        public string Nombre_Jerarquia { get; set; } = string.Empty;

        [Column("nivel_poder_minimo")]
        public int Nivel_Poder_Minimo { get; set; }

        [Column("nivel_poder_maximo")]
        public int? Nivel_Poder_Maximo { get; set; }

        [Column("descripcion")]
        public string? Descripcion { get; set; }
    }
}