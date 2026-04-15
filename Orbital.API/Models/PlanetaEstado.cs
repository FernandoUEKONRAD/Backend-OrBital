using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("estado_planeta")]
    public class PlanetaEstado
    {
        [Key]
        [Column("id_estado")]
        public int Id_Estado { get; set; }

        [Column("nombre_estado")]
        public string Nombre { get; set; } = null!;

        [Column("descripcion")]
        public string? Descripcion { get; set; }
    }
}