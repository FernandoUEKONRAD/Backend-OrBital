namespace Orbital.API.DTOs
{
    public class PlanetaCreateDto
    {
        public string Nombre { get; set; } = null!;
        public int? Id_Galaxia { get; set; }
        public NivelTecnologico Nivel_Tecnologico { get; set; }
        public int? Id_Atmosfera { get; set; }
        public long? Poblacion { get; set; }
        public string Nivel_Vida_Planeta { get; set; } = "Bajo";
        public int Id_Estado { get; set; } = 1;
        public int? Id_Propietario { get; set; }
        public string? Conquistado_Por { get; set; }
        public DateTime Fecha_Descubrimiento { get; set; }
        public string? Descripcion { get; set; }
        public string? Color1 { get; set; }
        public string? Color2 { get; set; }
        public string? Color3 { get; set; }
        public bool Activo { get; set; } = true;
        public decimal? CoordenadaX { get; set; }
        public decimal? CoordenadaY { get; set; }
        public decimal? CoordenadaZ { get; set; }
    }
}
