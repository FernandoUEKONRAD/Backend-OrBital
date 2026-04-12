using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;

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

        [HttpGet]
        public async Task<IActionResult> GetJerarquias()
        {
            var jerarquias = await _context.Jerarquias.ToListAsync();
            return Ok(jerarquias);
        }
    }
}