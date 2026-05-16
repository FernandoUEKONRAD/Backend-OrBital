namespace Orbital.API.DTOs
{
    public class PlanetaEstadoResponseDto
    {
        public int Id_Estado { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}