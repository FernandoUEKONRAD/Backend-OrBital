using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("recurso_planetario")]
    public class RecursoPlanetario
    {
        [Key]
        [Column("id_recurso")]
        public int Id_Recurso { get; set; }

        [Column("id_planeta")]
        [Required]
        public int Id_Planeta { get; set; }

        [Column("tipo_recurso")]
        [Required]
        public string Tipo_Recurso { get; set; } = null!;

        [Column("nombre_recurso")]
        [Required]
        public string Nombre_Recurso { get; set; } = null!;

        [Column("cantidad_estimada")]
        public decimal Cantidad_Estimada { get; set; } = 0.00m;

        [Column("unidad_medida")]
        public string Unidad_Medida { get; set; } = "unidades";

        [Column("valor_unitario")]
        public decimal Valor_Unitario { get; set; } = 0.00m;

        [Column("rareza")]
        public string Rareza { get; set; } = "Común";

        [Column("extraible")]
        public bool Extraible { get; set; } = true;

        [Column("fecha_registro")]
        public DateTime Fecha_Registro { get; set; } = DateTime.Now;

        // =========================
        // RELACIONES
        // =========================
        [ForeignKey("Id_Planeta")]
        public Planeta? Planeta { get; set; }
    }
}
