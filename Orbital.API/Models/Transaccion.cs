using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("transaccion")]
    public class Transaccion
    {
        [Key]
        [Column("id_transaccion")]
        public int Id_Transaccion { get; set; }

        [Column("id_publicacion")]
        public int Id_Publicacion { get; set; }

        [Column("id_comprador")]
        public int Id_Comprador { get; set; }

        [Column("id_vendedor")]
        public int Id_Vendedor { get; set; }

        [Column("precio_final")]
        public decimal Precio_Final { get; set; }

        [Column("fecha_transaccion")]
        public DateTime Fecha_Transaccion { get; set; }

        [Column("estado_transaccion")]
        [Required]
        public string Estado_Transaccion { get; set; } = "Pendiente";

        [Column("metodo_pago")]
        [Required]
        public string Metodo_Pago { get; set; } = null!;

        [Column("notas")]
        public string? Notas { get; set; }
    }
}
