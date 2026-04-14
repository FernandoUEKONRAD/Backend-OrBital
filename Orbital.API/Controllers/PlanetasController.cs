using Microsoft.AspNetCore.Mvc;
using Orbital.API.DTOs;
using Orbital.API.Services;

namespace Orbital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanetasController : ControllerBase
    {
        private readonly IPlanetasService _service;
        private readonly ILogger<PlanetasController> _logger;

        public PlanetasController(
            IPlanetasService service, 
            ILogger<PlanetasController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // =========================
        // GET ALL
        // =========================
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosPlanetas()
        {
            try
            {
                var planetas = await _service.ObtenerTodosPlanetas();

                return Ok(new
                {
                    message = "Planetas obtenidos exitosamente",
                    data = planetas
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener planetas");

                return StatusCode(500, new
                {
                    message = "Error interno al obtener planetas",
                    error = ex.Message
                });
            }
        }

        // =========================
        // GET BY ID
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPlanetaPorId(int id)
        {
            try
            {
                var planeta = await _service.ObtenerPlanetaPorId(id);

                if (planeta == null)
                    return NotFound(new { message = "Planeta no encontrado" });

                return Ok(new
                {
                    message = "Planeta obtenido exitosamente",
                    data = planeta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener planeta {Id}", id);

                return StatusCode(500, new
                {
                    message = "Error interno al obtener planeta",
                    error = ex.Message
                });
            }
        }

        // =========================
        // CREATE
        // =========================
        [HttpPost]
        public async Task<IActionResult> CrearPlaneta([FromBody] PlanetaCreateDto dto)
        {
            try
            {
                var planetaCreado = await _service.CrearPlaneta(dto);

                return CreatedAtAction(
                    nameof(ObtenerPlanetaPorId),
                    new { id = planetaCreado.Id_Planeta },
                    new
                    {
                        message = "Planeta creado exitosamente",
                        data = planetaCreado
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear planeta");

                return StatusCode(500, new
                {
                    message = "Error interno al crear planeta",
                    error = ex.Message
                });
            }
        }

        // =========================
        // UPDATE
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPlaneta(int id, [FromBody] PlanetaUpdateDto dto)
        {
            try
            {
                var actualizado = await _service.ActualizarPlaneta(id, dto);

                return Ok(new
                {
                    message = "Planeta actualizado exitosamente",
                    data = actualizado
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar planeta {Id}", id);

                return StatusCode(500, new
                {
                    message = "Error interno al actualizar planeta",
                    error = ex.Message
                });
            }
        }

        // =========================
        // DELETE
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPlaneta(int id)
        {
            try
            {
                var eliminado = await _service.EliminarPlaneta(id);

                if (!eliminado)
                    return NotFound(new { message = "Planeta no encontrado" });

                return Ok(new
                {
                    message = "Planeta eliminado exitosamente"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar planeta {Id}", id);

                return StatusCode(500, new
                {
                    message = "Error interno al eliminar planeta",
                    error = ex.Message
                });
            }
        }
    }
}