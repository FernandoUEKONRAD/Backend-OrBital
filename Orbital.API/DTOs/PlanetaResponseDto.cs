namespace Orbital.API.DTOs
{
    // Kept for backwards compatibility on Create/Update responses
    public class PlanetaResponseDto
    {
        public int Id_Planeta { get; set; }
        public string Nombre { get; set; } = null!;
        public int? Id_Galaxia { get; set; }
        public string NivelTecnologico { get; set; } = null!;
        public int? Id_Atmosfera { get; set; }
        public long? Poblacion { get; set; }
        public string Nivel_Vida_Planeta { get; set; } = null!;
        public int Id_Estado { get; set; }
        public int? Id_Propietario { get; set; }
        public string? Conquistado_Por { get; set; }
        public DateTime Fecha_Descubrimiento { get; set; }
        public string? Descripcion { get; set; }
        public string? Color1 { get; set; }
        public string? Color2 { get; set; }
        public string? Color3 { get; set; }
        public bool Activo { get; set; }
    }
}
