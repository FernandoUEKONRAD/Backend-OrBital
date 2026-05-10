using Orbital.API.DTOs;
using Orbital.API.Models;
using Orbital.API.Repositories;

namespace Orbital.API.Services
{
    public class TipoAtmosferaService
    {
        private readonly ITipoAtmosferaRepository _repo;

        public TipoAtmosferaService(ITipoAtmosferaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<SimpleItemDto>> ObtenerTodas()
        {
            var items = await _repo.ObtenerTodas();
            return items.Select(t => new SimpleItemDto { Id = t.Id_Atm, Nombre = t.Nombre });
        }
    }
}
