namespace Orbital.API.DTOs
{
    public class RecursoCreateDto
    {
        public string Nombre { get; set; } = null!;
        public string Tipo_Recurso { get; set; } = null!;
        public string Unidad_Medida { get; set; } = "unidades";
        public string Rareza { get; set; } = "Común";
    }

    public class RecursoUpdateDto
    {
        public string? Nombre { get; set; }
        public string? Tipo_Recurso { get; set; }
        public string? Unidad_Medida { get; set; }
        public string? Rareza { get; set; }
    }

    public class RecursoResponseDto
    {
        public int Id_Recurso { get; set; }
        public string Nombre { get; set; } = null!;
        public string Tipo_Recurso { get; set; } = null!;
        public string Unidad_Medida { get; set; } = null!;
        public string Rareza { get; set; } = null!;
    }
}