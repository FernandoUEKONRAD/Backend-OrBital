using Orbital.API.DTOs;

namespace Orbital.API.Services
{
    public interface IReporteService
    {
        Task<ReporteVentasDto> ReporteVentas(DateTime fechaInicio, DateTime fechaFin);
    }
}
