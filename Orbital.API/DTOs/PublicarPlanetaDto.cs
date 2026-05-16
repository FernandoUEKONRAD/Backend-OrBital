namespace Orbital.API.DTOs
{
    public class PublicarPlanetaDto
    {
        public int Id_Planeta { get; set; }
        public int Id_Valoracion { get; set; }
        public decimal Precio_Publicado { get; set; }
        public decimal Precio_Minimo { get; set; }
        public DateTime? Fecha_Vencimiento { get; set; }
        public string? Descripcion_Venta { get; set; }
    }
}
