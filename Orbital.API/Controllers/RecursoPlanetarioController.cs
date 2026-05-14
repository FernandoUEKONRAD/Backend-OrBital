using Microsoft.AspNetCore.Mvc;
using Orbital.API.DTOs;
using Orbital.API.Services;

namespace Orbital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecursosPlanetariosController : ControllerBase
    {
        private readonly IRecursoPlanetarioService _service;
        private readonly ILogger<RecursosPlanetariosController> _logger;

        public RecursosPlanetariosController(
            IRecursoPlanetarioService service,
            ILogger<RecursosPlanetariosController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // =========================
        // GET BY PLANETA
        // =========================
        [HttpGet("planeta/{idPlaneta}")]
        public async Task<IActionResult> ObtenerPorPlaneta(int idPlaneta)
        {
            try
            {
                var recursos = await _service.ObtenerPorPlaneta(idPlaneta);
                return Ok(new
                {
                    message = "Recursos del planeta obtenidos exitosamente",
                    data = recursos
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener recursos del planeta {Id}", idPlaneta);
                return StatusCode(500, new
                {
                    message = "Error interno al obtener recursos del planeta",
                    error = ex.Message
                });
            }
        }

        // =========================
        // GET BY ID
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var rp = await _service.ObtenerPorId(id);

                if (rp == null)
                    return NotFound(new { message = "Recurso planetario no encontrado" });

                return Ok(new
                {
                    message = "Recurso planetario obtenido exitosamente",
                    data = rp
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener recurso planetario {Id}", id);
                return StatusCode(500, new
                {
                    message = "Error interno al obtener recurso planetario",
                    error = ex.Message
                });
            }
        }

        // =========================
        // CREATE
        // =========================
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] RecursoPlanetarioCreateDto dto)
        {
            try
            {
                var creado = await _service.Crear(dto);
                return CreatedAtAction(
                    nameof(ObtenerPorId),
                    new { id = creado.Id_Recurso_Planeta },
                    new
                    {
                        message = "Recurso asignado al planeta exitosamente",
                        data = creado
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al asignar recurso al planeta");
                return StatusCode(500, new
                {
                    message = "Error interno al asignar recurso al planeta",
                    error = ex.Message
                });
            }
        }

        // =========================
        // UPDATE
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] RecursoPlanetarioUpdateDto dto)
        {
            try
            {
                var actualizado = await _service.Actualizar(id, dto);
                return Ok(new
                {
                    message = "Recurso planetario actualizado exitosamente",
                    data = actualizado
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar recurso planetario {Id}", id);
                return StatusCode(500, new
                {
                    message = "Error interno al actualizar recurso planetario",
                    error = ex.Message
                });
            }
        }

        // =========================
        // DELETE
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var eliminado = await _service.Eliminar(id);

                if (!eliminado)
                    return NotFound(new { message = "Recurso planetario no encontrado" });

                return Ok(new { message = "Recurso planetario eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar recurso planetario {Id}", id);
                return StatusCode(500, new
                {
                    message = "Error interno al eliminar recurso planetario",
                    error = ex.Message
                });
            }
        }
    }
}