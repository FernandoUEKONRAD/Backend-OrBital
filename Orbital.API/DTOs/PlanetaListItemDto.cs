namespace Orbital.API.DTOs
{
    public class PlanetaListItemDto
    {
        public int Id_Planeta { get; set; }
        public string Nombre { get; set; } = null!;
        public CoordenadasDto? Coordenadas { get; set; }
        public string? TipoAtmosfera { get; set; }
        public string NivelTecnologico { get; set; } = null!;
        public long? Poblacion { get; set; }
        public string Estado { get; set; } = null!;
        public List<RecursoPlanetaDto> Recursos { get; set; } = new();
        public string NivelDificultadConquista { get; set; } = null!;
    }
}
