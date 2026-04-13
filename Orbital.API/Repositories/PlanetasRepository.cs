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
            _context.Planetas.Add(planeta);
            await _context.SaveChangesAsync();
            return planeta;
        }

        public async Task<List<Planeta>> ObtenerTodosPlanetas()
        {
            return await _context.Planetas.ToListAsync();
        }

        public async Task<Planeta> ObtenerPlanetaPorId(int id)
        {
            var planeta = await _context.Planetas
                .FirstOrDefaultAsync(p => p.Id_Planeta == id);

            if (planeta == null)
                throw new Exception("Planeta no encontrado");

            return planeta;
        }

        public async Task<Planeta> ActualizarPlaneta(int id, Planeta input)
        {
            var planeta = await _context.Planetas
                .FirstOrDefaultAsync(p => p.Id_Planeta == id);

            if (planeta == null)
                throw new Exception("Planeta no encontrado");

            planeta.Nombre = input.Nombre;
            planeta.Sistema_Estelar = input.Sistema_Estelar;
            planeta.Galaxia = input.Galaxia;
            planeta.Nivel_Tecnologico = input.Nivel_Tecnologico;
            planeta.Atmosfera = input.Atmosfera;
            planeta.Poblacion = input.Poblacion;
            planeta.Nivel_Vida_Nativa = input.Nivel_Vida_Nativa;
            planeta.Id_Estado = input.Id_Estado;
            planeta.Id_Propietario = input.Id_Propietario;
            planeta.Fecha_Descubrimiento = input.Fecha_Descubrimiento;
            planeta.Coordenadas = input.Coordenadas;
            planeta.Descripcion = input.Descripcion;
            planeta.Activo = input.Activo;

            await _context.SaveChangesAsync();
            return planeta;
        }

        public async Task<bool> EliminarPlaneta(int id)
        {
            var planeta = await _context.Planetas
                .FirstOrDefaultAsync(p => p.Id_Planeta == id);

            if (planeta == null)
                return false;

            _context.Planetas.Remove(planeta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}