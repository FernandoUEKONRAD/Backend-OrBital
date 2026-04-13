namespace Orbital.API.DTOs
{
    public class PlanetaUpdateDto
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public double? Diametro { get; set; }
        public string? Tipo { get; set; }
        public double? DistanciaAlSol { get; set; }
        public int? TiempoOrbita { get; set; }
        public bool? TieneAtmosfera { get; set; }
        public int? NumeroLunas { get; set; }
        public bool? Habitable { get; set; }
        public string? Estado { get; set; }
    }
}
