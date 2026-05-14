using Microsoft.EntityFrameworkCore;
using Orbital.API.Data;
using Orbital.API.Models;

namespace Orbital.API.Repositories
{
    public interface IPlanetasRepository
    {
        Task<Planeta> CrearPlaneta(Planeta planeta);
        Task<List<Planeta>> ObtenerTodosPlanetas(
            int? idPlaneta = null,
            string? nombre = null,
            int? idAtmosfera = null,
            NivelTecnologico? nivelTecnologico = null,
            long? poblacionMin = null,
            long? poblacionMax = null,
            int? idEstado = null,
            string? tipoRecurso = null);
        Task<List<Planeta>> ObtenerPlanetasPorGalaxia(int galaxiaId);
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

        public async Task<List<Planeta>> ObtenerTodosPlanetas(
            int? idPlaneta = null,
            string? nombre = null,
            int? idAtmosfera = null,
            NivelTecnologico? nivelTecnologico = null,
            long? poblacionMin = null,
            long? poblacionMax = null,
            int? idEstado = null,
            string? tipoRecurso = null)
        {
            var query = _context.Planetas
                .Include(p => p.Estado)
                .Include(p => p.GalaxiaNav)
                .Include(p => p.AtmosferaNav)
                .Include(p => p.Coordenadas)
                .Include(p => p.Recursos)
                    .ThenInclude(rp => rp.Recurso)
                .AsQueryable();

            if (idPlaneta.HasValue)
                query = query.Where(p => p.Id_Planeta == idPlaneta.Value);

            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(p => p.Nombre.Contains(nombre));

            if (idAtmosfera.HasValue)
                query = query.Where(p => p.Id_Atmosfera == idAtmosfera.Value);

            if (nivelTecnologico.HasValue)
                query = query.Where(p => p.Nivel_Tecnologico == nivelTecnologico.Value);

            if (poblacionMin.HasValue)
                query = query.Where(p => p.Poblacion >= poblacionMin.Value);

            if (poblacionMax.HasValue)
                query = query.Where(p => p.Poblacion <= poblacionMax.Value);

            if (idEstado.HasValue)
                query = query.Where(p => p.Id_Estado == idEstado.Value);

            if (!string.IsNullOrWhiteSpace(tipoRecurso))
                query = query.Where(p => p.Recursos.Any(rp => rp.Recurso != null && rp.Recurso.Tipo_Recurso == tipoRecurso));

            return await query.ToListAsync();
        }

        public async Task<List<Planeta>> ObtenerPlanetasPorGalaxia(int galaxiaId)
        {
            return await _context.Planetas
                .Include(p => p.Coordenadas)
                .Where(p => p.Id_Galaxia == galaxiaId && p.Activo)
                .ToListAsync();
        }

        public async Task<Planeta?> ObtenerPlanetaPorId(int id)
        {
            return await _context.Planetas
                .Include(p => p.Estado)
                .Include(p => p.GalaxiaNav)
                .Include(p => p.AtmosferaNav)
                .Include(p => p.Coordenadas)
                .Include(p => p.Recursos)
                    .ThenInclude(rp => rp.Recurso)
                .Include(p => p.Misiones)
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
