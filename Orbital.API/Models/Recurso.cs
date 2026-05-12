using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("recurso")]
    public class Recurso
    {
        [Key]
        [Column("id_recurso")]
        public int Id_Recurso { get; set; }

        [Column("nombre")]
        [Required]
        public string Nombre { get; set; } = null!;

        [Column("tipo_recurso")]
        [Required]
        public string Tipo_Recurso { get; set; } = null!;

        [Column("unidad_medida")]
        public string Unidad_Medida { get; set; } = "unidades";

        [Column("rareza")]
        public string Rareza { get; set; } = "Común";
    }
}