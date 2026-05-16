using Microsoft.AspNetCore.Mvc;
using Orbital.API.DTOs;
using Orbital.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Orbital.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService service, ILogger<AuthController> logger)
        {
            _service = service;
            _logger = logger;
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
                return StatusCode(201, new
                {
                    usuario.Id_Usuario,
                    usuario.Nombre,
                    usuario.Correo,
                    usuario.Activo,
                    usuario.Fecha_Registro
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                var inner = ex.InnerException?.Message ?? ex.Message;
                _logger.LogError(ex, "Error de base de datos al registrar usuario");

                if (inner.Contains("Duplicate") || inner.Contains("duplicate") || inner.Contains("unique"))
                    return Conflict(new { message = "Ya existe un usuario registrado con ese correo." });

                return StatusCode(500, new { message = "Error de base de datos.", detalle = inner });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al registrar usuario");
                return StatusCode(500, new { message = "Error inesperado.", detalle = ex.Message });
            }
        }
    }
}