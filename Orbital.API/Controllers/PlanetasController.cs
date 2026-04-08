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

        public PlanetasController(IPlanetasService service, ILogger<PlanetasController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los planetas registrados
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosPlanetas()
        {
            try
            {
                var planetas = await _service.ObtenerTodosPlanetas();
                return Ok(new
                {
                    message = "Planetas obtenidos exitosamente",
                    cantidad = planetas.Count,
                    data = planetas
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los planetas");
                return StatusCode(500, new { message = "Ocurrió un error al obtener los planetas" });
            }
        }

        /// <summary>
        /// Obtiene un planeta por su ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPlanetaPorId(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "El ID del planeta debe ser mayor a 0" });

                var planeta = await _service.ObtenerPlanetaPorId(id);
                return Ok(new
                {
                    message = "Planeta obtenido exitosamente",
                    data = planeta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener planeta por ID: {IdPlaneta}", id);
                return StatusCode(500, new { message = "Ocurrió un error al obtener el planeta" });
            }
        }

        /// <summary>
        /// Crea un nuevo planeta
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CrearPlaneta([FromBody] PlanetaCreateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { message = "Datos incompletos o inválidos", errors = ModelState });

                var planetaCreado = await _service.CrearPlaneta(dto);
                return CreatedAtAction(nameof(ObtenerPlanetaPorId), 
                    new { id = planetaCreado.Id }, 
                    new
                    {
                        message = "Planeta creado exitosamente",
                        data = planetaCreado
                    });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al crear planeta");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear planeta");
                return StatusCode(500, new { message = "Ocurrió un error al crear el planeta" });
            }
        }

        /// <summary>
        /// Actualiza un planeta existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPlaneta(int id, [FromBody] PlanetaUpdateDto dto)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "El ID del planeta debe ser mayor a 0" });

                if (!ModelState.IsValid)
                    return BadRequest(new { message = "Datos inválidos", errors = ModelState });

                var planetaActualizado = await _service.ActualizarPlaneta(id, dto);
                return Ok(new
                {
                    message = "Planeta actualizado exitosamente",
                    data = planetaActualizado
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al actualizar planeta ID: {IdPlaneta}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar planeta ID: {IdPlaneta}", id);
                return StatusCode(500, new { message = "Ocurrió un error al actualizar el planeta" });
            }
        }

        /// <summary>
        /// Elimina un planeta
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPlaneta(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "El ID del planeta debe ser mayor a 0" });

                var resultado = await _service.EliminarPlaneta(id);
                
                if (!resultado)
                    return NotFound(new { message = "Planeta no encontrado" });

                return Ok(new { message = "Planeta eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar planeta ID: {IdPlaneta}", id);
                return StatusCode(500, new { message = "Ocurrió un error al eliminar el planeta" });
            }
        }
    }
}