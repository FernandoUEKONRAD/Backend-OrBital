using Microsoft.AspNetCore.Mvc;
using Orbital.API.DTOs;
using Orbital.API.Services;
using Microsoft.AspNetCore.Authorization;
using Orbital.API.Authorization;

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
            _logger  = logger;
        }

        // =========================
        // GET ALL (con filtros)
        // Endpoint 3: Listar todos los planetas con condiciones por query
        // =========================
        [Authorize(Policy = Policies.PlanetasRead)]
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosPlanetas(
            [FromQuery] int? idPlaneta        = null,
            [FromQuery] string? nombre        = null,
            [FromQuery] int? idAtmosfera      = null,
            [FromQuery] NivelTecnologico? nivelTecnologico = null,
            [FromQuery] long? poblacionMin    = null,
            [FromQuery] long? poblacionMax    = null,
            [FromQuery] int? idEstado         = null,
            [FromQuery] string? tipoRecurso   = null)
        {
            try
            {
                var planetas = await _service.ObtenerTodosPlanetas(
                    idPlaneta, nombre, idAtmosfera, nivelTecnologico,
                    poblacionMin, poblacionMax, idEstado, tipoRecurso);

                return Ok(new
                {
                    message = "Planetas obtenidos exitosamente",
                    data    = planetas
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener planetas");
                return StatusCode(500, new { message = "Error interno al obtener planetas", error = ex.Message });
            }
        }

        // =========================
        // GET BY GALAXIA
        // Endpoint 2: Listar planetas por galaxia
        // =========================
        [Authorize(Policy = Policies.PlanetasRead)]
        [HttpGet("galaxia/{galaxiaId:int}")]
        public async Task<IActionResult> ObtenerPlanetasPorGalaxia(int galaxiaId)
        {
            try
            {
                var planetas = await _service.ObtenerPlanetasPorGalaxia(galaxiaId);

                return Ok(new
                {
                    message = "Planetas de la galaxia obtenidos exitosamente",
                    data    = planetas
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener planetas por galaxia {GalaxiaId}", galaxiaId);
                return StatusCode(500, new { message = "Error interno al obtener planetas por galaxia", error = ex.Message });
            }
        }

        // =========================
        // GET BY ID (detalle completo)
        // Endpoint 4: Obtener toda la información de un planeta
        // =========================
        [Authorize(Policy = Policies.PlanetasRead)]
        [HttpGet("{id:int}")]
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
                    data    = planeta
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener planeta {Id}", id);
                return StatusCode(500, new { message = "Error interno al obtener planeta", error = ex.Message });
            }
        }

        // =========================
        // CREATE
        // =========================
        [Authorize(Policy = Policies.PlanetasCreate)]
        [HttpPost]
        public async Task<IActionResult> CrearPlaneta([FromBody] PlanetaCreateDto dto)
        {
            try
            {
                var planetaCreado = await _service.CrearPlaneta(dto);

                return CreatedAtAction(
                    nameof(ObtenerPlanetaPorId),
                    new { id = planetaCreado.Id_Planeta },
                    new { message = "Planeta creado exitosamente", data = planetaCreado });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear planeta");
                return StatusCode(500, new { message = "Error interno al crear planeta", error = ex.Message });
            }
        }

        // =========================
        // UPDATE
        // =========================
        [Authorize(Policy = Policies.PlanetasUpdate)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> ActualizarPlaneta(int id, [FromBody] PlanetaUpdateDto dto)
        {
            try
            {
                var actualizado = await _service.ActualizarPlaneta(id, dto);

                return Ok(new
                {
                    message = "Planeta actualizado exitosamente",
                    data    = actualizado
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Planeta no encontrado" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar planeta {Id}", id);
                return StatusCode(500, new { message = "Error interno al actualizar planeta", error = ex.Message });
            }
        }

        // =========================
        // DELETE
        // =========================
        [Authorize(Policy = Policies.PlanetasDelete)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarPlaneta(int id)
        {
            try
            {
                var eliminado = await _service.EliminarPlaneta(id);

                if (!eliminado)
                    return NotFound(new { message = "Planeta no encontrado" });

                return Ok(new { message = "Planeta eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar planeta {Id}", id);
                return StatusCode(500, new { message = "Error interno al eliminar planeta", error = ex.Message });
            }
        }
    }
}
