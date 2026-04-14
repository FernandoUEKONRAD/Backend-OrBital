namespace Orbital.API.Models
{
    public class Planeta
    {
        public int Id_Planeta { get; set; }

        public string Nombre { get; set; } = null!;
        public string? Sistema_Estelar { get; set; }
        public string? Galaxia { get; set; }

        public int Nivel_Tecnologico { get; set; }

        public string? Atmosfera { get; set; }
        public long? Poblacion { get; set; }

        public string Nivel_Vida_Nativa { get; set; } = "Bajo";

        public int Id_Estado { get; set; }
        public int? Id_Propietario { get; set; }

        public DateTime Fecha_Descubrimiento { get; set; }

        public string? Coordenadas { get; set; }
        public string? Descripcion { get; set; }

        public bool Activo { get; set; }

        // =========================
        // RELACIONES
        // =========================
        public PlanetaEstado? Estado { get; set; }
    }
}