using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;
using Orbital.API.Models;

namespace Orbital.API.Repositories
{
    public interface IRecursoPlanetarioRepository
    {
        Task<RecursoPlaneta> Crear(RecursoPlaneta recursoPlanetario);
        Task<List<RecursoPlaneta>> ObtenerPorPlaneta(int idPlaneta);
        Task<RecursoPlaneta?> ObtenerPorId(int id);
        Task<RecursoPlaneta> Actualizar(RecursoPlaneta recursoPlanetario);
        Task<bool> Eliminar(int id);
    }

    public class RecursoPlanetarioRepository : IRecursoPlanetarioRepository
    {
        private readonly AppDbContext _context;

        public RecursoPlanetarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RecursoPlaneta> Crear(RecursoPlaneta recursoPlanetario)
        {
            _context.RecursosPlaneta.Add(recursoPlanetario);
            await _context.SaveChangesAsync();
            return recursoPlanetario;
        }

        public async Task<List<RecursoPlaneta>> ObtenerPorPlaneta(int idPlaneta)
        {
            return await _context.RecursosPlaneta
                .Include(rp => rp.Recurso)
                .Where(rp => rp.Id_Planeta == idPlaneta)
                .ToListAsync();
        }

        public async Task<RecursoPlaneta?> ObtenerPorId(int id)
        {
            return await _context.RecursosPlaneta
                .Include(rp => rp.Recurso)
                .FirstOrDefaultAsync(rp => rp.Id_Recurso_Planeta == id);
        }

        public async Task<RecursoPlaneta> Actualizar(RecursoPlaneta recursoPlanetario)
        {
            _context.RecursosPlaneta.Update(recursoPlanetario);
            await _context.SaveChangesAsync();
            return recursoPlanetario;
        }

        public async Task<bool> Eliminar(int id)
        {
            var rp = await _context.RecursosPlaneta
                .FirstOrDefaultAsync(rp => rp.Id_Recurso_Planeta == id);

            if (rp == null) return false;

            _context.RecursosPlaneta.Remove(rp);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
