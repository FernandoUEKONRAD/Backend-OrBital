using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;
using Orbital.API.DTOs;
using Orbital.API.Services;

namespace Orbital.API.Controllers;

[ApiController]
[Route("api/planetas/{id}/estado")]
public class PlanetaEstadoController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly PlanetaEstadoService _estadoService;
    private readonly ILogger<PlanetaEstadoController> _logger;

    public PlanetaEstadoController(
        AppDbContext db,
        PlanetaEstadoService estadoService,
        ILogger<PlanetaEstadoController> logger)
    {
        _db = db;
        _estadoService = estadoService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerEstado(int id)
    {
        try
        {
            var planeta = await _db.Planetas
                .FirstOrDefaultAsync(p => p.Id == id);

            if (planeta == null)
                return NotFound(new { message = "Planeta no encontrado" });


            var estadoActual = planeta.Estado ?? "Disponible";
            var siguientes = _estadoService.ObtenerSiguientesEstados(estadoActual);

            return Ok(new
            {
                planetaId = planeta.Id,
                nombre = planeta.Nombre,
                estadoActual,
                siguientesEstadosPermitidos = siguientes
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener estado ID: {id}", id);
            return StatusCode(500, new { message = "Error al obtener estado" });
        }
    }

    [HttpPut]
    public async Task<IActionResult> CambiarEstado(int id, [FromBody] CambiarEstadoDto dto)
    {
        try
        {
            if (string.IsNullOrEmpty(dto.NuevoEstado))
                return BadRequest(new { message = "El nuevo estado es requerido" });

            var planeta = await _db.Planetas
                .FirstOrDefaultAsync(p => p.Id == id);

            if (planeta == null)
                return NotFound(new { message = "Planeta no encontrado" });

            var estadoActual = planeta.Estado ?? "Disponible";

            // Validar que el estado nuevo existe en la BD
            var estadoNuevoExiste = await _db.EstadosPlaneta
                .AnyAsync(e => e.NombreEstado == dto.NuevoEstado);

            if (!estadoNuevoExiste)
                return BadRequest(new
                {
                    message = $"'{dto.NuevoEstado}' no es un estado válido",
                    estadosValidos = _estadoService.ObtenerTodosLosEstados()
                });

            // Validar flujo
            if (!_estadoService.CambioEsValido(estadoActual, dto.NuevoEstado))
                return BadRequest(new
                {
                    message = $"No se puede cambiar de '{estadoActual}' a '{dto.NuevoEstado}'",
                    siguientesPermitidos = _estadoService.ObtenerSiguientesEstados(estadoActual)
                });

            // Guardar
            string anterior = estadoActual;
            planeta.Estado = dto.NuevoEstado;
            await _db.SaveChangesAsync();

            return Ok(new
            {
                message = "Estado actualizado correctamente",
                data = new
                {
                    planetaId = planeta.Id,
                    nombre = planeta.Nombre,
                    estadoAnterior = anterior,
                    estadoNuevo = planeta.Estado
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cambiar estado ID: {id}", id);
            return StatusCode(500, new { message = "Error al cambiar estado" });
        }
    }
}