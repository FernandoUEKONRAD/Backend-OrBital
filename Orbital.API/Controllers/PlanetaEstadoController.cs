using Microsoft.AspNetCore.Mvc;
using Orbital.API.DTOs;
using Orbital.API.Services;

namespace Orbital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanetaEstadoController : ControllerBase
    {
        private readonly PlanetaEstadoService _service;

        public PlanetaEstadoController(PlanetaEstadoService service)
        {
            _service = service;
        }

        // =========================
        // GET ALL
        // =========================
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var estados = await _service.ObtenerEstados();
            return Ok(estados);
        }

        // =========================
        // GET BY ID
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var estado = await _service.ObtenerEstadoPorId(id);

            if (estado == null)
                return NotFound("Estado no encontrado");

            return Ok(estado);
        }

        // =========================
        // CREATE
        // =========================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlanetaEstadoCreateDto dto)
        {
            var creado = await _service.CrearEstado(dto);
            return Ok(creado);
        }

        // =========================
        // UPDATE
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PlanetaEstadoUpdateDto dto)
        {
            var actualizado = await _service.ActualizarEstado(id, dto);
            return Ok(actualizado);
        }

        // =========================
        // DELETE
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _service.EliminarEstado(id);

            if (!eliminado)
                return NotFound("Estado no encontrado");

            return Ok("Estado eliminado correctamente");
        }
    }
}