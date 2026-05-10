using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orbital.API.DTOs;
using Orbital.API.Services;

namespace Orbital.API.Controllers
{
    [ApiController]
    [Route("api/catalogos")]
    public class CatalogosController : ControllerBase
    {
        private readonly TipoAtmosferaService _atmService;
        private readonly GalaxiaService _galService;
        private readonly PlanetaEstadoService _estadoService;

        public CatalogosController(
            TipoAtmosferaService atmService,
            GalaxiaService galService,
            PlanetaEstadoService estadoService)
        {
            _atmService = atmService;
            _galService = galService;
            _estadoService = estadoService;
        }

        [Authorize]
        [HttpGet("atmosferas")]
        public async Task<IActionResult> ListarAtmosferas()
        {
            var items = await _atmService.ObtenerTodas();
            return Ok(items);
        }

        [Authorize]
        [HttpGet("niveles-tecnologicos")]
        public IActionResult ListarNivelesTecnologicos()
        {
            var values = Enum.GetValues(typeof(Orbital.API.Models.NivelTecnologico))
                .Cast<Orbital.API.Models.NivelTecnologico>()
                .Select(v => new SimpleItemDto { Id = (int)v, Nombre = v.ToString() });

            return Ok(values);
        }

        [Authorize]
        [HttpGet("estados")]
        public async Task<IActionResult> ListarEstados()
        {
            var items = await _estadoService.ObtenerEstados();
            return Ok(items);
        }

        [Authorize]
        [HttpGet("galaxias")]
        public async Task<IActionResult> ListarGalaxias()
        {
            var items = await _galService.ObtenerTodas();
            return Ok(items);
        }
    }
}
