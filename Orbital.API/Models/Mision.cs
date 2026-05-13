using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("mision")]
    public class Mision
    {
        [Key]
        [Column("id_mision")]
        public int Id_Mision { get; set; }

        [Column("id_planeta")]
        public int Id_Planeta { get; set; }

        [Column("id_equipo")]
        public int Id_Equipo { get; set; }

        [Column("id_comandante")]
        public int Id_Comandante { get; set; }

        [Column("nombre_mision")]
        public string Nombre_Mision { get; set; } = null!;

        [Column("tipo_mision")]
        public string Tipo_Mision { get; set; } = null!;

        [Column("prioridad")]
        public byte Prioridad { get; set; } = 3;

        [Column("id_estado_mision")]
        public int Id_Estado_Mision { get; set; }

        [Column("fecha_asignacion")]
        public DateTime Fecha_Asignacion { get; set; }

        [Column("fecha_inicio")]
        public DateTime? Fecha_Inicio { get; set; }

        [Column("fecha_fin_estimada")]
        public DateTime? Fecha_Fin_Estimada { get; set; }

        [Column("fecha_fin_real")]
        public DateTime? Fecha_Fin_Real { get; set; }

        [Column("porcentaje_avance")]
        public decimal Porcentaje_Avance { get; set; } = 0.00m;

        [Column("recompensa_ofrecida")]
        public decimal Recompensa_Ofrecida { get; set; } = 0.00m;

        [Column("observaciones")]
        public string? Observaciones { get; set; }
    }
}
