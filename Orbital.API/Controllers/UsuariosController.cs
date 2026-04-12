using Microsoft.AspNetCore.Mvc;
using Orbital.API.Services;

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
    }
}