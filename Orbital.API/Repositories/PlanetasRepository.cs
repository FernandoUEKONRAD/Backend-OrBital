using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;
using Orbital.API.Models;

namespace Orbital.API.Repositories
{
    public interface IPlanetasRepository
    {
        Task<Planeta> CrearPlaneta(Planeta planeta);
        Task<List<Planeta>> ObtenerTodosPlanetas();
        Task<Planeta?> ObtenerPlanetaPorId(int id);
        Task<Planeta> ActualizarPlaneta(Planeta planeta);
        Task<bool> EliminarPlaneta(int id);
    }

    public class PlanetasRepository : IPlanetasRepository
    {
        private readonly AppDbContext _context;

        public PlanetasRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Planeta> CrearPlaneta(Planeta planeta)
        {
            _context.Planetas.Add(planeta);
            await _context.SaveChangesAsync();
            return planeta;
        }

        public async Task<List<Planeta>> ObtenerTodosPlanetas()
        {
            return await _context.Planetas.ToListAsync();
        }

        public async Task<Planeta?> ObtenerPlanetaPorId(int id)
        {
            return await _context.Planetas
                .FirstOrDefaultAsync(p => p.Id_Planeta == id);
        }

        public async Task<Planeta> ActualizarPlaneta(Planeta planeta)
        {
            _context.Planetas.Update(planeta);
            await _context.SaveChangesAsync();
            return planeta;
        }

        public async Task<bool> EliminarPlaneta(int id)
        {
            var planeta = await _context.Planetas.FirstOrDefaultAsync(p => p.Id_Planeta == id);

            if (planeta == null) return false;

            _context.Planetas.Remove(planeta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}