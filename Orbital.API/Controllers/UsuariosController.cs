using Microsoft.AspNetCore.Mvc;
using Orbital.API.Services;
using Microsoft.AspNetCore.Authorization;
using Orbital.API.DTOs;

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

        [Authorize(Policy = "EmperadorOnly")]
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _service.GetUsuarios();
            return Ok(usuarios);
        }

        [Authorize(Policy = "EmperadorOnly")]
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
