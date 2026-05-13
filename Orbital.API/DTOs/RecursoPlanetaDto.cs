namespace Orbital.API.DTOs
{
    public class RecursoPlanetaDto
    {
        public int Id_Recurso { get; set; }
        public string Nombre { get; set; } = null!;
        public string Tipo_Recurso { get; set; } = null!;
        public string Unidad_Medida { get; set; } = null!;
        public string Rareza { get; set; } = null!;
        public decimal Cantidad_Estimada { get; set; }
        public decimal Valor_Unitario { get; set; }
        public bool Extraible { get; set; }
    }
}
