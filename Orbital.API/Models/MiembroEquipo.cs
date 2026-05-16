using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("miembro_equipo")]
    public class MiembroEquipo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_miembro")]
        public int Id_Miembro { get; set; }

        [Column("id_equipo")]
        public int Id_Equipo { get; set; }

        [Column("id_usuario")]
        public int Id_Usuario { get; set; }

        [Column("nivel_poder")]
        public int Nivel_Poder { get; set; }

        [Column("rol_equipo")]
        public string Rol_Equipo { get; set; } = string.Empty;

        [Column("fecha_ingreso")]
        public DateTime Fecha_Ingreso { get; set; }

        [Column("activo")]
        public bool Activo { get; set; }

        [ForeignKey("Id_Usuario")]
        public Usuario Usuario { get; set; } = null!;
    }
}