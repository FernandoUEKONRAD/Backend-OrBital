using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;
using Orbital.API.Models;

namespace Orbital.API.Repositories
{
    public interface IRecursoPlanetarioRepository
    {
        Task<RecursoPlanetario> Crear(RecursoPlanetario recursoPlanetario);
        Task<List<RecursoPlanetario>> ObtenerPorPlaneta(int idPlaneta);
        Task<RecursoPlanetario?> ObtenerPorId(int id);
        Task<RecursoPlanetario> Actualizar(RecursoPlanetario recursoPlanetario);
        Task<bool> Eliminar(int id);
    }

    public class RecursoPlanetarioRepository : IRecursoPlanetarioRepository
    {
        private readonly AppDbContext _context;

        public RecursoPlanetarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RecursoPlanetario> Crear(RecursoPlanetario recursoPlanetario)
        {
            _context.RecursosPlanetarios.Add(recursoPlanetario);
            await _context.SaveChangesAsync();
            return recursoPlanetario;
        }

        public async Task<List<RecursoPlanetario>> ObtenerPorPlaneta(int idPlaneta)
        {
            return await _context.RecursosPlanetarios
                .Include(rp => rp.Recurso)
                .Where(rp => rp.Id_Planeta == idPlaneta)
                .ToListAsync();
        }

        public async Task<RecursoPlanetario?> ObtenerPorId(int id)
        {
            return await _context.RecursosPlanetarios
                .Include(rp => rp.Recurso)
                .FirstOrDefaultAsync(rp => rp.Id_Recurso_Planeta == id);
        }

        public async Task<RecursoPlanetario> Actualizar(RecursoPlanetario recursoPlanetario)
        {
            _context.RecursosPlanetarios.Update(recursoPlanetario);
            await _context.SaveChangesAsync();
            return recursoPlanetario;
        }

        public async Task<bool> Eliminar(int id)
        {
            var rp = await _context.RecursosPlanetarios
                .FirstOrDefaultAsync(rp => rp.Id_Recurso_Planeta == id);

            if (rp == null) return false;

            _context.RecursosPlanetarios.Remove(rp);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}