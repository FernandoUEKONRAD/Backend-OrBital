using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("planeta_valoracion")]
    public class PlanetaValoracion
    {
        [Key]
        [Column("id_valoracion")]
        public int Id_Valoracion { get; set; }

        [Column("id_planeta")]
        [Required]
        public int Id_Planeta { get; set; }

        [Column("recursos_score")]
        public decimal Recursos_Score { get; set; } = 0.00m;

        [Column("tecnologia_score")]
        public decimal Tecnologia_Score { get; set; } = 0.00m;

        [Column("ubicacion_score")]
        public decimal Ubicacion_Score { get; set; } = 0.00m;

        [Column("poder_score")]
        public decimal Poder_Score { get; set; } = 0.00m;

        [Column("riesgo_score")]
        public decimal Riesgo_Score { get; set; } = 0.00m;

        [Column("valor_total")]
        public decimal Valor_Total { get; set; } = 0.00m;

        [Column("clase_planeta")]
        public string Clase_Planeta { get; set; } = "D";

        [Column("precio_final")]
        public decimal Precio_Final { get; set; } = 0.00m;

        [Column("id_analista")]
        [Required]
        public int Id_Analista { get; set; }

        [Column("fecha_valoracion")]
        public DateTime Fecha_Valoracion { get; set; } = DateTime.Now;

        [Column("aprobado_por")]
        public int? Aprobado_Por { get; set; }

        [Column("fecha_aprobacion")]
        public DateTime? Fecha_Aprobacion { get; set; }

        [Column("estado_valoracion")]
        public string Estado_Valoracion { get; set; } = "Pendiente";

        [Column("observaciones")]
        public string? Observaciones { get; set; }

        // =========================
        // RELACIONES
        // =========================
        [ForeignKey("Id_Planeta")]
        public Planeta? Planeta { get; set; }

        [ForeignKey("Id_Analista")]
        public Usuario? Analista { get; set; }

        [ForeignKey("Aprobado_Por")]
        public Usuario? AprobadoPor { get; set; }
    }
}
