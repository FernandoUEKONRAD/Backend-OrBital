namespace Orbital.API.DTOs
{
    public class CambiarEstadoTransaccionDto
    {
        /// <summary>Valores válidos: Completada, Anulada, EnDisputa</summary>
        public string Estado { get; set; } = null!;
        public string? Notas { get; set; }
    }
}
