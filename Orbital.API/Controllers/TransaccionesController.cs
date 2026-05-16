using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orbital.API.Authorization;
using Orbital.API.DTOs;
using Orbital.API.Services;
using System.Security.Claims;

namespace Orbital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionesController : ControllerBase
    {
        private readonly ITransaccionService _service;
        private readonly ILogger<TransaccionesController> _logger;

        public TransaccionesController(ITransaccionService service, ILogger<TransaccionesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // =========================
        // POST - COMPRAR PLANETA (cliente autenticado)
        // =========================
        [Authorize(Policy = Policies.ClienteAutenticado)]
        [HttpPost("publicacion/{idPublicacion}/comprar")]
        public async Task<IActionResult> Comprar(int idPublicacion, [FromBody] ComprarPlanetaDto dto)
        {
            try
            {
                if (idPublicacion <= 0)
                    return BadRequest(new { message = "ID de publicación inválido" });

                if (string.IsNullOrWhiteSpace(dto.Metodo_Pago))
                    return BadRequest(new { message = "El método de pago es requerido" });

                var idCliente = ObtenerIdCliente();
                if (idCliente <= 0)
                    return Unauthorized(new { message = "Token de cliente inválido" });

                var ip = ObtenerIp();
                var resultado = await _service.ComprarPlaneta(idPublicacion, idCliente, dto, ip);

                return Ok(new
                {
                    message = "Compra registrada exitosamente. Estado: Pendiente",
                    data = resultado
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Error al procesar compra de publicación {Id}", idPublicacion);
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al comprar planeta publicación {Id}", idPublicacion);
                return StatusCode(500, new { message = "Error interno al procesar compra", error = ex.Message });
            }
        }

        // =========================
        // PATCH - CONFIRMAR / ANULAR COMPRA (gestor interno)
        // =========================
        [Authorize(Policy = Policies.TransaccionesGestionar)]
        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] CambiarEstadoTransaccionDto dto)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "ID de transacción inválido" });

                if (string.IsNullOrWhiteSpace(dto.Estado))
                    return BadRequest(new { message = "El estado es requerido" });

                var idUsuario = ObtenerIdUsuarioInterno();
                var ip = ObtenerIp();
                var resultado = await _service.CambiarEstadoTransaccion(id, dto, idUsuario, ip);

                return Ok(new
                {
                    message = $"Estado de transacción actualizado a '{dto.Estado}' exitosamente",
                    data = resultado
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validación al cambiar estado transacción {Id}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cambiar estado transacción {Id}", id);
                return StatusCode(500, new { message = "Error interno al cambiar estado", error = ex.Message });
            }
        }

        // =========================
        // GET - HISTORIAL GLOBAL (administrador/emperador)
        // =========================
        [Authorize(Policy = Policies.TransaccionesLeer)]
        [HttpGet]
        public async Task<IActionResult> Listar(
            [FromQuery] string? estado,
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin,
            [FromQuery] int? idComprador)
        {
            try
            {
                var lista = await _service.ListarTransacciones(estado, fechaInicio, fechaFin, idComprador);

                return Ok(new
                {
                    message = "Historial de transacciones obtenido exitosamente",
                    cantidad = lista.Count,
                    filtros = new { estado, fechaInicio, fechaFin, idComprador },
                    data = lista
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar transacciones");
                return StatusCode(500, new { message = "Error interno al listar transacciones", error = ex.Message });
            }
        }

        // =========================
        // GET - COMPRAS DE UN CLIENTE (cliente autenticado o admin)
        // =========================
        [Authorize]
        [HttpGet("cliente/{idCliente}")]
        public async Task<IActionResult> ComprasCliente(int idCliente)
        {
            try
            {
                if (idCliente <= 0)
                    return BadRequest(new { message = "ID de cliente inválido" });

                // Un cliente solo puede ver sus propias compras; un gestor interno puede ver cualquiera
                var esCliente = User.FindFirstValue("tipo") == "cliente";
                if (esCliente)
                {
                    var idTokenCliente = ObtenerIdCliente();
                    if (idTokenCliente != idCliente)
                        return Forbid();
                }

                var compras = await _service.ListarComprasCliente(idCliente);

                return Ok(new
                {
                    message = "Historial de compras obtenido exitosamente",
                    cantidad = compras.Count,
                    data = compras
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener compras del cliente {Id}", idCliente);
                return StatusCode(500, new { message = "Error interno al obtener compras", error = ex.Message });
            }
        }

        private int ObtenerIdCliente()
        {
            var claim = User.FindFirstValue("Id_Cliente");
            return int.TryParse(claim, out var id) ? id : 0;
        }

        private int ObtenerIdUsuarioInterno()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue("sub") ?? "0";
            return int.TryParse(claim, out var id) ? id : 0;
        }

        private string ObtenerIp() =>
            HttpContext.Connection.RemoteIpAddress?.ToString() ?? "desconocida";
    }
}
