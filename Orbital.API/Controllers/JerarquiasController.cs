using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;
using Orbital.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Orbital.API.Authorization;

namespace Orbital.API.Controllers
{
    [ApiController]
    [Route("api/jerarquias")]
    public class JerarquiasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JerarquiasController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Policy = Policies.JerarquiasRead)]
        [HttpGet]
        public async Task<IActionResult> GetJerarquias()
        {
            var jerarquias = await _context.Jerarquias
                .Select(j => new JerarquiaDto
                {
                    Id_Jerarquia = j.Id_Jerarquia,
                    Nombre_Jerarquia = j.Nombre_Jerarquia,
                    Descripcion = j.Descripcion,
                    Nivel_Poder_Minimo = j.Nivel_Poder_Minimo,
                    Nivel_Poder_Maximo = j.Nivel_Poder_Maximo
                })
                .ToListAsync();

            return Ok(jerarquias);
        }
    }
}