using Microsoft.AspNetCore.Mvc;
using Orbital.API.DTOs;
using Orbital.API.Services;

namespace Orbital.API.Controllers
{
    [ApiController]
    [Route("api/auth/cliente")]
    public class ClienteAuthController : ControllerBase
    {
        private readonly IClienteAuthService _authService;
        private readonly ILogger<ClienteAuthController> _logger;

        public ClienteAuthController(IClienteAuthService authService, ILogger<ClienteAuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        // =========================
        // POST - REGISTRAR CLIENTE
        // =========================
        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] ClienteRegistroDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    return BadRequest(new { message = "El nombre es requerido" });

                if (string.IsNullOrWhiteSpace(dto.Correo))
                    return BadRequest(new { message = "El correo es requerido" });

                if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 6)
                    return BadRequest(new { message = "La contraseña debe tener al menos 6 caracteres" });

                var resultado = await _authService.Registrar(dto);

                return StatusCode(201, new
                {
                    message = "Cliente registrado exitosamente",
                    data = resultado
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Intento de registro con correo duplicado: {Correo}", dto.Correo);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar cliente");
                return StatusCode(500, new { message = "Error interno al registrar cliente", error = ex.Message });
            }
        }

        // =========================
        // POST - LOGIN CLIENTE
        // =========================
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ClienteLoginDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Correo) || string.IsNullOrWhiteSpace(dto.Password))
                    return BadRequest(new { message = "Correo y contraseña son requeridos" });

                var resultado = await _authService.Login(dto);

                if (resultado == null)
                    return Unauthorized(new { message = "Correo o contraseña incorrectos" });

                return Ok(new
                {
                    message = "Login exitoso",
                    data = resultado
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al autenticar cliente");
                return StatusCode(500, new { message = "Error interno al autenticar", error = ex.Message });
            }
        }
    }
}
