namespace Orbital.API.DTOs
{
    public class PlanetaGalaxiaItemDto
    {
        public int Id_Planeta { get; set; }
        public string Nombre { get; set; } = null!;
        public CoordenadasDto? Coordenadas { get; set; }
    }
}
