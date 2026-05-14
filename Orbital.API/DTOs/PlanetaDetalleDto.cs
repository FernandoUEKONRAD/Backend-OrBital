namespace Orbital.API.DTOs
{
    public class PlanetaDetalleDto
    {
        public int Id_Planeta { get; set; }
        public string Nombre { get; set; } = null!;
        public CoordenadasDto? Coordenadas { get; set; }
        public string? TipoAtmosfera { get; set; }
        public string NivelTecnologico { get; set; } = null!;
        public long? Poblacion { get; set; }
        public string Estado { get; set; } = null!;
        public List<RecursoPlanetaDto> Recursos { get; set; } = new();
        public string NivelVidaPlaneta { get; set; } = null!;
        public string? Galaxia { get; set; }
        public string? Descripcion { get; set; }
        public DateTime Fecha_Descubrimiento { get; set; }
        public List<MisionResumenDto> Misiones { get; set; } = new();
        public string? Color1 { get; set; }
        public string? Color2 { get; set; }
        public string? Color3 { get; set; }
        public string? Conquistado_Por { get; set; }
        public bool Activo { get; set; }
    }
}
