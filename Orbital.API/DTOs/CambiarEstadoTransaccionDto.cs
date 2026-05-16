namespace Orbital.API.DTOs
{
    public class CambiarEstadoTransaccionDto
    {
        /// <summary>Valores válidos: Completada, Anulada, En Disputa</summary>
        public string Estado { get; set; } = null!;
        public string? Notas { get; set; }
    }
}
