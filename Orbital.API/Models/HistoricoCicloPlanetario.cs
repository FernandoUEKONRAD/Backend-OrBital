using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("historico_ciclo_planetario")]
    public class HistoricoCicloPlanetario
    {
        [Key]
        [Column("id_historico")]
        public int Id_Historico { get; set; }

        [Column("id_planeta")]
        [Required]
        public int Id_Planeta { get; set; }

        [Column("id_estado_anterior")]
        [Required]
        public int Id_Estado_Anterior { get; set; }

        [Column("id_estado_nuevo")]
        [Required]
        public int Id_Estado_Nuevo { get; set; }

        [Column("id_usuario_cambio")]
        [Required]
        public int Id_Usuario_Cambio { get; set; }

        [Column("id_cliente_externo")]
        public int? Id_Cliente_Externo { get; set; }

        [Column("motivo")]
        public string? Motivo { get; set; }

        [Column("id_transaccion")]
        public int? Id_Transaccion { get; set; }

        [Column("id_cliente_anterior")]
        public int? Id_Cliente_Anterior { get; set; }

        [Column("id_cliente_nuevo")]
        public int? Id_Cliente_Nuevo { get; set; }

        [Column("tipo_evento")]
        [Required]
        public string Tipo_Evento { get; set; } = null!;

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Column("fecha_cambio")]
        public DateTime Fecha_Cambio { get; set; } = DateTime.Now;

        [ForeignKey("Id_Planeta")]
        public Planeta? Planeta { get; set; }

        [ForeignKey("Id_Transaccion")]
        public Transaccion? Transaccion { get; set; }

        [ForeignKey("Id_Cliente_Anterior")]
        public Cliente? ClienteAnterior { get; set; }

        [ForeignKey("Id_Cliente_Nuevo")]
        public Cliente? ClienteNuevo { get; set; }
    }
}
