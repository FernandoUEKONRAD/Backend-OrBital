namespace Orbital.API.DTOs
{
    public class PlanetaGalaxiaItemDto
    {
        public int Id_Planeta { get; set; }
        public string Nombre { get; set; } = null!;
        public CoordenadasDto? Coordenadas { get; set; }
        public string? Color1 { get; set; }
        public string? Color2 { get; set; }
        public string? Color3 { get; set; }
    }
}
