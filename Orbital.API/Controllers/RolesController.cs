using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;

namespace Orbital.API.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RolesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }
    }
}