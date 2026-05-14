using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("amenaza_deteccion")]
    public class AmenazaDeteccion
    {
        [Key]
        [Column("id_amenaza")]
        public int Id_Amenaza { get; set; }

        [Column("id_planeta")]
        public int Id_Planeta { get; set; }

        [Column("id_mision")]
        public int? Id_Mision { get; set; }

        [Column("tipo_amenaza")]
        [Required]
        public string Tipo_Amenaza { get; set; } = null!;

        [Column("nivel_peligro")]
        public byte Nivel_Peligro { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; } = null!;

        [Column("fecha_deteccion")]
        public DateTime Fecha_Deteccion { get; set; }

        [Column("id_detectado_por")]
        public int? Id_Detectado_Por { get; set; }

        [Column("estado_amenaza")]
        [Required]
        public string Estado_Amenaza { get; set; } = "Activa";

        [Column("protocolo_activado")]
        public string? Protocolo_Activado { get; set; }
    }
}
