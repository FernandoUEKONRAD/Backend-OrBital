using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("cliente")]
    public class Cliente
    {
        [Key]
        [Column("id_cliente")]
        public int Id_Cliente { get; set; }

        [Column("nombre")]
        [Required]
        public string Nombre { get; set; } = null!;

        [Column("tipo_cliente")]
        [Required]
        public string Tipo_Cliente { get; set; } = "Individuo";

        [Column("id_galaxia_origen")]
        public int? Id_Galaxia_Origen { get; set; }

        [Column("correo")]
        [Required]
        public string Correo { get; set; } = null!;

        [Column("contrasena_hash")]
        [Required]
        public string Contrasena_Hash { get; set; } = null!;

        [Column("credito_disponible")]
        public decimal Credito_Disponible { get; set; } = 0.00m;

        [Column("nivel_confianza")]
        public string Nivel_Confianza { get; set; } = "Nuevo";

        [Column("fecha_registro")]
        public DateTime Fecha_Registro { get; set; } = DateTime.Now;

        [Column("activo")]
        public bool Activo { get; set; } = true;

        [ForeignKey("Id_Galaxia_Origen")]
        public Galaxia? GalaxiaOrigen { get; set; }
    }
}
