using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orbital.API.Models
{
    [Table("planeta")]
    public class Planeta
    {
        [Key]
        [Column("id_planeta")]
        public int Id_Planeta { get; set; }

        [Column("nombre")]
        [Required]
        public string Nombre { get; set; } = null!;

        [Column("id_galaxia")]
        public int? Id_Galaxia { get; set; }

        [Column("id_nivel_tecnologico")]
        public NivelTecnologico Nivel_Tecnologico { get; set; }

        [Column("id_atmosfera")]
        public int? Id_Atmosfera { get; set; }

        [Column("poblacion")]
        public long? Poblacion { get; set; }

        [Column("nivel_vida_planeta")]
        public string Nivel_Vida_Planeta { get; set; } = "Bajo";

        [Column("id_estado")]
        public int Id_Estado { get; set; }

        [Column("id_propietario")]
        public int? Id_Propietario { get; set; }

        [Column("conquistado_por")]
        public string? Conquistado_Por { get; set; }

        [Column("fecha_descubrimiento")]
        public DateTime Fecha_Descubrimiento { get; set; }

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Column("color1")]
        public string? Color1 { get; set; }

        [Column("color2")]
        public string? Color2 { get; set; }

        [Column("color3")]
        public string? Color3 { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;

        // Navigation properties
        public PlanetaEstado? Estado { get; set; }
        public Galaxia? GalaxiaNav { get; set; }
        public TipoAtmosfera? AtmosferaNav { get; set; }
        public CoordenadasPlaneta? Coordenadas { get; set; }
        public ICollection<RecursoPlaneta> Recursos { get; set; } = new List<RecursoPlaneta>();
        public ICollection<Mision> Misiones { get; set; } = new List<Mision>();
    }
}

public enum NivelTecnologico : int
{
    Primitivo    = 1,
    Medieval     = 2,
    Avanzado     = 3,
    Interestelar = 4
}
