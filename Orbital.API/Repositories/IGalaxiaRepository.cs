using Orbital.API.Models;

namespace Orbital.API.Repositories
{
    public interface IGalaxiaRepository
    {
        Task<IEnumerable<Galaxia>> ObtenerTodas();
    }
}
