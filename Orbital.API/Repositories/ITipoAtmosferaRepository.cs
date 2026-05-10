using Orbital.API.Models;

namespace Orbital.API.Repositories
{
    public interface ITipoAtmosferaRepository
    {
        Task<IEnumerable<TipoAtmosfera>> ObtenerTodas();
    }
}
