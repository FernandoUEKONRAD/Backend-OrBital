namespace Orbital.API.DTOs
{
    public class MercadoListItemDto
    {
        public int Id_Publicacion { get; set; }
        public int Id_Planeta { get; set; }
        public string Nombre_Planeta { get; set; } = null!;
        public string Galaxia { get; set; } = null!;
        public decimal Precio_Publicado { get; set; }
        public string Clase_Planeta { get; set; } = null!;
        public string? Descripcion_Venta { get; set; }
        public DateTime? Fecha_Vencimiento { get; set; }
        public DateTime Fecha_Publicacion { get; set; }
    }
}
