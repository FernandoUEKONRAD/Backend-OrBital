namespace Orbital.API.DTOs
{
    public class ReporteVentasDto
    {
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Fin { get; set; }
        public int Total_Planetas_Vendidos { get; set; }
        public decimal Total_Ingresos { get; set; }
        public string? Planeta_Mas_Caro { get; set; }
        public decimal? Precio_Mas_Alto { get; set; }
        public List<VentasPorClaseDto> Ventas_Por_Clase { get; set; } = new();
    }

    public class VentasPorClaseDto
    {
        public string Clase { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal Total_Ingresos { get; set; }
    }
}
