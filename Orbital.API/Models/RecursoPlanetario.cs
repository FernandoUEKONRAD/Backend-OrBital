using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("recurso_planeta")]
    public class RecursoPlanetario
    {
        [Key]
        [Column("id_recurso_planeta")]
        public int Id_Recurso_Planeta { get; set; }

        [Column("id_planeta")]
        [Required]
        public int Id_Planeta { get; set; }

        [Column("id_recurso")]
        [Required]
        public int Id_Recurso { get; set; }

        [Column("cantidad_estimada")]
        public decimal Cantidad_Estimada { get; set; } = 0.00m;

        [Column("valor_unitario")]
        public decimal Valor_Unitario { get; set; } = 0.00m;

        [Column("extraible")]
        public bool Extraible { get; set; } = true;

        [Column("fecha_registro")]
        public DateTime Fecha_Registro { get; set; } = DateTime.Now;

        // =========================
        // RELACIONES
        // =========================
        [ForeignKey("Id_Planeta")]
        public Planeta? Planeta { get; set; }

        [ForeignKey("Id_Recurso")]
        public Recurso? Recurso { get; set; }
    }
}