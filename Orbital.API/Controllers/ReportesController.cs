using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orbital.API.Authorization;
using Orbital.API.Services;

namespace Orbital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService _service;
        private readonly ILogger<ReportesController> _logger;

        public ReportesController(IReporteService service, ILogger<ReportesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // =========================
        // GET - REPORTE DE VENTAS POR PERÍODO
        // =========================
        [Authorize(Policy = Policies.ReportesLeer)]
        [HttpGet("ventas")]
        public async Task<IActionResult> ReporteVentas(
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin)
        {
            try
            {
                var inicio = fechaInicio ?? DateTime.Now.AddMonths(-1);
                var fin = fechaFin ?? DateTime.Now;

                if (inicio > fin)
                    return BadRequest(new { message = "La fecha de inicio no puede ser mayor a la fecha de fin" });

                var reporte = await _service.ReporteVentas(inicio, fin);

                return Ok(new
                {
                    message = "Reporte de ventas generado exitosamente",
                    data = reporte
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar reporte de ventas");
                return StatusCode(500, new { message = "Error interno al generar reporte", error = ex.Message });
            }
        }
    }
}
