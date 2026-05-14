namespace Orbital.API.DTOs
{
    public class RecursoPlanetarioCreateDto
    {
        public int Id_Planeta { get; set; }
        public int Id_Recurso { get; set; }
        public decimal Cantidad_Estimada { get; set; } = 0.00m;
        public decimal Valor_Unitario { get; set; } = 0.00m;
        public bool Extraible { get; set; } = true;
    }

    public class RecursoPlanetarioUpdateDto
    {
        public decimal? Cantidad_Estimada { get; set; }
        public decimal? Valor_Unitario { get; set; }
        public bool? Extraible { get; set; }
    }

    public class RecursoPlanetarioResponseDto
    {
        public int Id_Recurso_Planeta { get; set; }
        public int Id_Planeta { get; set; }
        public int Id_Recurso { get; set; }

        // Datos del recurso (del catálogo)
        public string Nombre_Recurso { get; set; } = null!;
        public string Tipo_Recurso { get; set; } = null!;
        public string Unidad_Medida { get; set; } = null!;
        public string Rareza { get; set; } = null!;

        public decimal Cantidad_Estimada { get; set; }
        public decimal Valor_Unitario { get; set; }
        public bool Extraible { get; set; }
        public DateTime Fecha_Registro { get; set; }
    }
}