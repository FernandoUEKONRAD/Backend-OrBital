using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("auditoria")]
    public class Auditoria
    {
        [Key]
        [Column("id_auditoria")]
        public long Id_Auditoria { get; set; }

        [Column("id_usuario")]
        public int? Id_Usuario { get; set; }

        [Column("accion")]
        [Required]
        public string Accion { get; set; } = null!;

        [Column("tabla_afectada")]
        public string? Tabla_Afectada { get; set; }

        [Column("id_registro_afectado")]
        public int? Id_Registro_Afectado { get; set; }

        [Column("valor_anterior")]
        public string? Valor_Anterior { get; set; }

        [Column("valor_nuevo")]
        public string? Valor_Nuevo { get; set; }

        [Column("timestamp_accion")]
        public DateTime Timestamp_Accion { get; set; } = DateTime.UtcNow;

        [Column("ip_origen")]
        public string? Ip_Origen { get; set; }

        [Column("user_agent")]
        public string? User_Agent { get; set; }

        [Column("resultado")]
        public string Resultado { get; set; } = "Exitoso";
    }
}
