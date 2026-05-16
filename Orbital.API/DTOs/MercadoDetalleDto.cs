namespace Orbital.API.DTOs
{
    public class MercadoDetalleDto
    {
        public int Id_Publicacion { get; set; }
        public int Id_Planeta { get; set; }
        public string Nombre_Planeta { get; set; } = null!;
        public string? Descripcion_Planeta { get; set; }
        public string? Descripcion_Venta { get; set; }
        public decimal Precio_Publicado { get; set; }
        public DateTime? Fecha_Vencimiento { get; set; }
        public DateTime Fecha_Publicacion { get; set; }

        // Scores de valoración
        public decimal Recursos_Score { get; set; }
        public decimal Tecnologia_Score { get; set; }
        public decimal Ubicacion_Score { get; set; }
        public decimal Poder_Score { get; set; }
        public decimal Riesgo_Score { get; set; }
        public string Clase_Planeta { get; set; } = null!;

        // Recursos
        public List<RecursoPlanetaMercadoDto> Recursos { get; set; } = new();

        // Coordenadas
        public CoordenadasMercadoDto? Coordenadas { get; set; }

        // Atmósfera
        public string? Tipo_Atmosfera { get; set; }
        public string? Descripcion_Atmosfera { get; set; }
    }

    public class RecursoPlanetaMercadoDto
    {
        public string Nombre_Recurso { get; set; } = null!;
        public decimal Cantidad_Estimada { get; set; }
        public string Rareza { get; set; } = null!;
    }

    public class CoordenadasMercadoDto
    {
        public decimal Coordenada_X { get; set; }
        public decimal Coordenada_Y { get; set; }
        public decimal Coordenada_Z { get; set; }
    }
}
