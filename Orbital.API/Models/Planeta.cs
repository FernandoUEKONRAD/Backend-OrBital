namespace Orbital.API.Models
{
    public class Planeta
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public double Diametro { get; set; }
        public required string Tipo { get; set; } // Terrestre, Gaseoso, etc
        public double DistanciaAlSol { get; set; } // en millones de km
        public int TiempoOrbita { get; set; } // en días
        public bool TieneAtmosfera { get; set; }
        public int NumeroLunas { get; set; }
        public bool Habitable { get; set; }
        public string Estado { get; set; } = "Activo"; // Activo, Inactivo, etc
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public DateTime? FechaActualizacion { get; set; }
    }
}
