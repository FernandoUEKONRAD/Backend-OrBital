using Microsoft.AspNetCore.Mvc;
using Orbital.API.Services;
using Orbital.API.DTOs;

namespace Orbital.API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _service.GetUsuarios();
            return Ok(usuarios);
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
