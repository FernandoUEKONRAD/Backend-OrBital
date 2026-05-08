namespace Orbital.API.DTOs
{
    public class ValoracionPlanetaResponseDto
    {
        public int Id_Valoracion { get; set; }
        public int Id_Planeta { get; set; }
        public string Nombre_Planeta { get; set; } = null!;

        // ===== SCORES =====
        public decimal Recursos_Score { get; set; }
        public decimal Tecnologia_Score { get; set; }
        public decimal Ubicacion_Score { get; set; }
        public decimal Poder_Score { get; set; }
        public decimal Riesgo_Score { get; set; }

        // ===== RESULTADO FINAL =====
        public decimal Valor_Total { get; set; }
        public string Clase_Planeta { get; set; } = null!;
        public decimal Precio_Final { get; set; }

        // ===== METADATA =====
        public int Id_Analista { get; set; }
        public string Nombre_Analista { get; set; } = null!;
        public DateTime Fecha_Valoracion { get; set; }

        public int? Aprobado_Por { get; set; }
        public string? Nombre_Aprobador { get; set; }
        public DateTime? Fecha_Aprobacion { get; set; }

        public string Estado_Valoracion { get; set; } = null!;
        public string? Observaciones { get; set; }
    }
}
