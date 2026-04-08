namespace Orbital.API.DTOs
{
    public class PlanetaCreateDto
    {
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public double Diametro { get; set; }
        public required string Tipo { get; set; }
        public double DistanciaAlSol { get; set; }
        public int TiempoOrbita { get; set; }
        public bool TieneAtmosfera { get; set; }
        public int NumeroLunas { get; set; }
        public bool Habitable { get; set; }
    }
}
