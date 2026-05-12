using Microsoft.AspNetCore.Mvc;
using Orbital.API.DTOs;
using Orbital.API.Services;

namespace Orbital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecursosController : ControllerBase
    {
        private readonly IRecursoService _service;
        private readonly ILogger<RecursosController> _logger;

        public RecursosController(IRecursoService service, ILogger<RecursosController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // =========================
        // GET ALL
        // =========================
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var recursos = await _service.ObtenerTodos();
                return Ok(new
                {
                    message = "Recursos obtenidos exitosamente",
                    data = recursos
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener recursos");
                return StatusCode(500, new
                {
                    message = "Error interno al obtener recursos",
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
                var recurso = await _service.ObtenerPorId(id);

                if (recurso == null)
                    return NotFound(new { message = "Recurso no encontrado" });

                return Ok(new
                {
                    message = "Recurso obtenido exitosamente",
                    data = recurso
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener recurso {Id}", id);
                return StatusCode(500, new
                {
                    message = "Error interno al obtener recurso",
                    error = ex.Message
                });
            }
        }

        // =========================
        // CREATE
        // =========================
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] RecursoCreateDto dto)
        {
            try
            {
                var creado = await _service.Crear(dto);
                return CreatedAtAction(
                    nameof(ObtenerPorId),
                    new { id = creado.Id_Recurso },
                    new
                    {
                        message = "Recurso creado exitosamente",
                        data = creado
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear recurso");
                return StatusCode(500, new
                {
                    message = "Error interno al crear recurso",
                    error = ex.Message
                });
            }
        }

        // =========================
        // UPDATE
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] RecursoUpdateDto dto)
        {
            try
            {
                var actualizado = await _service.Actualizar(id, dto);
                return Ok(new
                {
                    message = "Recurso actualizado exitosamente",
                    data = actualizado
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar recurso {Id}", id);
                return StatusCode(500, new
                {
                    message = "Error interno al actualizar recurso",
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
                    return NotFound(new { message = "Recurso no encontrado" });

                return Ok(new { message = "Recurso eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar recurso {Id}", id);
                return StatusCode(500, new
                {
                    message = "Error interno al eliminar recurso",
                    error = ex.Message
                });
            }
        }
    }
}