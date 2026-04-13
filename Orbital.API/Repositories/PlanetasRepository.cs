using Orbital.API.Data;
using Orbital.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Orbital.API.Repositories
{
    public interface IPlanetasRepository
    {
        Task<Planeta> CrearPlaneta(Planeta planeta);
        Task<List<Planeta>> ObtenerTodosPlanetas();
        Task<Planeta> ObtenerPlanetaPorId(int id);
        Task<Planeta> ActualizarPlaneta(int id, Planeta planeta);
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
            try
            {
                _context.Planetas.Add(planeta);
                await _context.SaveChangesAsync();
                return planeta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el planeta en la base de datos", ex);
            }
        }

        public async Task<List<Planeta>> ObtenerTodosPlanetas()
        {
            try
            {
                return await _context.Planetas.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los planetas", ex);
            }
        }

        public async Task<Planeta> ObtenerPlanetaPorId(int id)
        {
            try
            {
                var planeta = await _context.Planetas.FirstOrDefaultAsync(p => p.Id == id);
                if (planeta == null)
                {
                    throw new Exception($"Planeta con ID {id} no encontrado");
                }
                return planeta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el planeta con ID {id}", ex);
            }
        }

        public async Task<Planeta> ActualizarPlaneta(int id, Planeta planetaActualizado)
        {
            try
            {
                var planeta = await _context.Planetas.FirstOrDefaultAsync(p => p.Id == id);
                if (planeta == null)
                {
                    throw new Exception($"Planeta con ID {id} no encontrado");
                }

                planeta.Nombre = planetaActualizado.Nombre ?? planeta.Nombre;
                planeta.Descripcion = planetaActualizado.Descripcion ?? planeta.Descripcion;
                planeta.Diametro = planetaActualizado.Diametro > 0 ? planetaActualizado.Diametro : planeta.Diametro;
                planeta.Tipo = planetaActualizado.Tipo ?? planeta.Tipo;
                planeta.DistanciaAlSol = planetaActualizado.DistanciaAlSol > 0 ? planetaActualizado.DistanciaAlSol : planeta.DistanciaAlSol;
                planeta.TiempoOrbita = planetaActualizado.TiempoOrbita > 0 ? planetaActualizado.TiempoOrbita : planeta.TiempoOrbita;
                planeta.TieneAtmosfera = planetaActualizado.TieneAtmosfera;
                planeta.NumeroLunas = planetaActualizado.NumeroLunas >= 0 ? planetaActualizado.NumeroLunas : planeta.NumeroLunas;
                planeta.Habitable = planetaActualizado.Habitable;
                planeta.Estado = planetaActualizado.Estado ?? planeta.Estado;
                planeta.FechaActualizacion = DateTime.UtcNow;

                _context.Planetas.Update(planeta);
                await _context.SaveChangesAsync();
                return planeta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el planeta con ID {id}", ex);
            }
        }

        public async Task<bool> EliminarPlaneta(int id)
        {
            try
            {
                var planeta = await _context.Planetas.FirstOrDefaultAsync(p => p.Id == id);
                if (planeta == null)
                {
                    return false;
                }

                _context.Planetas.Remove(planeta);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el planeta con ID {id}", ex);
            }
        }
    }
}
