using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;
using Orbital.API.Models;

namespace Orbital.API.Repositories
{
    public class PlanetaEstadoRepository : IPlanetaEstadoRepository
    {
        private readonly AppDbContext _context;

        public PlanetaEstadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlanetaEstado>> ObtenerEstados()
        {
            return await _context.PlanetaEstados.ToListAsync();
        }

        public async Task<PlanetaEstado?> ObtenerEstadoPorId(int id)
        {
            return await _context.PlanetaEstados.FindAsync(id);
        }

        public async Task<PlanetaEstado> CrearEstado(PlanetaEstado estado)
        {
            _context.PlanetaEstados.Add(estado);
            await _context.SaveChangesAsync();
            return estado;
        }

        public async Task<PlanetaEstado> ActualizarEstado(PlanetaEstado estado)
        {
            _context.PlanetaEstados.Update(estado);
            await _context.SaveChangesAsync();
            return estado;
        }

        public async Task<bool> EliminarEstado(int id)
        {
            var estado = await _context.PlanetaEstados.FindAsync(id);

            if (estado == null)
                return false;

            _context.PlanetaEstados.Remove(estado);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}