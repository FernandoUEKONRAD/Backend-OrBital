using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;
using Orbital.API.Models;

namespace Orbital.API.Repositories
{
    public class GalaxiaRepository : IGalaxiaRepository
    {
        private readonly AppDbContext _context;

        public GalaxiaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Galaxia>> ObtenerTodas()
        {
            return await _context.Galaxias.ToListAsync();
        }
    }
}
