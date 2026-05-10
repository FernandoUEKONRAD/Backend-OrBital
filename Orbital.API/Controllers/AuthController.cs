using Microsoft.AspNetCore.Mvc;
using Orbital.API.DTOs;
using Orbital.API.Services;
using Microsoft.AspNetCore.Authorization;

namespace Orbital.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(UsuarioLoginDto dto)
        {
            var result = await _service.Login(dto);

            if (result == null)
                return StatusCode(400, new
                {
                    message = "credenciales incorrectas, intenta de nuevo"
                });

            return StatusCode(200, result);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(UsuarioCreateDto dto)
        {
            try
            {
                var usuario = await _service.Register(dto);
                return CreatedAtAction(nameof(Register), new { id = usuario.Id_Usuario }, usuario);
            } catch (InvalidOperationException ex) {
                return BadRequest(new{
                    message = ex.Message
                });
            } catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error al registrar el usuario. Intenta de nuevo."
                });
            }
        }
    }
}