using Orbital.API.Models;

namespace Orbital.API.Repositories
{
    public interface IPlanetaEstadoRepository
    {
        Task<IEnumerable<PlanetaEstado>> ObtenerEstados();
        Task<PlanetaEstado?> ObtenerEstadoPorId(int id);
        Task<PlanetaEstado> CrearEstado(PlanetaEstado estado);
        Task<PlanetaEstado> ActualizarEstado(PlanetaEstado estado);
        Task<bool> EliminarEstado(int id);
    }
}