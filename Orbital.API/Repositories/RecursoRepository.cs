using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;
using Orbital.API.Models;

namespace Orbital.API.Repositories
{
    public interface IRecursoRepository
    {
        Task<Recurso> Crear(Recurso recurso);
        Task<List<Recurso>> ObtenerTodos();
        Task<Recurso?> ObtenerPorId(int id);
        Task<Recurso> Actualizar(Recurso recurso);
        Task<bool> Eliminar(int id);
    }

    public class RecursoRepository : IRecursoRepository
    {
        private readonly AppDbContext _context;

        public RecursoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Recurso> Crear(Recurso recurso)
        {
            _context.Recursos.Add(recurso);
            await _context.SaveChangesAsync();
            return recurso;
        }

        public async Task<List<Recurso>> ObtenerTodos()
        {
            return await _context.Recursos.ToListAsync();
        }

        public async Task<Recurso?> ObtenerPorId(int id)
        {
            return await _context.Recursos
                .FirstOrDefaultAsync(r => r.Id_Recurso == id);
        }

        public async Task<Recurso> Actualizar(Recurso recurso)
        {
            _context.Recursos.Update(recurso);
            await _context.SaveChangesAsync();
            return recurso;
        }

        public async Task<bool> Eliminar(int id)
        {
            var recurso = await _context.Recursos.FirstOrDefaultAsync(r => r.Id_Recurso == id);

            if (recurso == null) return false;

            _context.Recursos.Remove(recurso);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}