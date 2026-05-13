namespace Orbital.API.DTOs
{
    public class MisionResumenDto
    {
        public int Id_Mision { get; set; }
        public string Nombre_Mision { get; set; } = null!;
        public string Tipo_Mision { get; set; } = null!;
        public int Id_Estado_Mision { get; set; }
        public decimal Porcentaje_Avance { get; set; }
        public DateTime? Fecha_Inicio { get; set; }
        public DateTime? Fecha_Fin_Estimada { get; set; }
    }
}
