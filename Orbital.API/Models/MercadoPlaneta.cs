using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("mercado_planeta")]
    public class MercadoPlaneta
    {
        [Key]
        [Column("id_publicacion")]
        public int Id_Publicacion { get; set; }

        [Column("id_planeta")]
        public int Id_Planeta { get; set; }

        [Column("id_valoracion")]
        public int Id_Valoracion { get; set; }

        [Column("precio_publicado")]
        public decimal Precio_Publicado { get; set; }

        [Column("precio_minimo")]
        public decimal Precio_Minimo { get; set; }

        [Column("fecha_publicacion")]
        public DateTime Fecha_Publicacion { get; set; }

        [Column("fecha_vencimiento")]
        public DateTime? Fecha_Vencimiento { get; set; }

        [Column("activo")]
        public bool Activo { get; set; }

        [Column("id_publicado_por")]
        public int Id_Publicado_Por { get; set; }

        [Column("descripcion_venta")]
        public string? Descripcion_Venta { get; set; }

        [ForeignKey("Id_Planeta")]
        public Planeta? Planeta { get; set; }

        [ForeignKey("Id_Valoracion")]
        public PlanetaValoracion? Valoracion { get; set; }
    }
}
