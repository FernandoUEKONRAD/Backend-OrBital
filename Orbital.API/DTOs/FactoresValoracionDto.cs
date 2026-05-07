namespace Orbital.API.DTOs
{
    public class FactoresValoracionDto
    {
        public int Id_Valoracion { get; set; }
        public int Id_Planeta { get; set; }
        public string Nombre_Planeta { get; set; } = null!;

        // ===== DESGLOSE DE FACTORES =====
        public ScoreDetalleDto Recursos { get; set; } = null!;
        public ScoreDetalleDto Tecnologia { get; set; } = null!;
        public ScoreDetalleDto Ubicacion { get; set; } = null!;
        public ScoreDetalleDto Poder { get; set; } = null!;
        public ScoreDetalleDto Riesgo { get; set; } = null!;

        // ===== RESUMEN =====
        public decimal Valor_Total { get; set; }
        public string Clase_Planeta { get; set; } = null!;
        public decimal Precio_Final { get; set; }

        public string Estado_Valoracion { get; set; } = null!;
    }

    public class ScoreDetalleDto
    {
        public string Nombre { get; set; } = null!;
        public decimal Valor { get; set; }
        public string Descripcion { get; set; } = null!;
    }
}
