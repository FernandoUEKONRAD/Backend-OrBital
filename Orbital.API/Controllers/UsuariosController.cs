using Microsoft.AspNetCore.Mvc;
using Orbital.API.Services;
using Microsoft.AspNetCore.Authorization;
using Orbital.API.DTOs;
using Orbital.API.Authorization;

namespace Orbital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        [Authorize(Policy = Policies.UsuariosRead)]
        [HttpGet]
        public async Task<IActionResult> GetUsuarios(
            [FromQuery] string? nombre,
            [FromQuery] bool? activo,
            [FromQuery] DateTime? fechaDesde,
            [FromQuery] DateTime? fechaHasta,
            [FromQuery] int? jerarquiaId,
            [FromQuery] string? letra,
            [FromQuery] int? nivelPoderMin,
            [FromQuery] int? nivelPoderMax,
            [FromQuery] string? ordenarPor,
            [FromQuery] bool desc = false)
        {
            var usuarios = await _service.ListarUsuarios(
                nombre, activo, fechaDesde, fechaHasta,
                jerarquiaId, letra, nivelPoderMin, nivelPoderMax,
                ordenarPor, desc);

            return Ok(new
            {
                message = "Usuarios obtenidos exitosamente",
                cantidad = usuarios.Count,
                filtros = new { nombre, activo, fechaDesde, fechaHasta, jerarquiaId, letra, nivelPoderMin, nivelPoderMax, ordenarPor, desc },
                data = usuarios
            });
        }

        [Authorize(Policy = Policies.UsuariosRead)]
        [HttpGet("/ultimos")]
        public async Task<IActionResult> ObtenerUltimos3UsuariosPorRol()
        {
            var result = await _service.ObtenerUltimos3UsuariosPorRol();
            return Ok(result);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUsuario(int id, [FromBody] UsuarioUpdateDto dto)
        {
            var usuario = await _service.GetUsuarioById(id);
            if (usuario == null)
                return NotFound();

            await _service.UpdateUsuario(id, dto);
            return NoContent();
        }
    }
}
