using Orbital.API.DTOs;
using Orbital.API.Models;
using Orbital.API.Repositories;

namespace Orbital.API.Services
{
    public class GalaxiaService
    {
        private readonly IGalaxiaRepository _repo;

        public GalaxiaService(IGalaxiaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<SimpleItemDto>> ObtenerTodas()
        {
            var items = await _repo.ObtenerTodas();
            return items.Select(g => new SimpleItemDto { Id = g.Id_Galaxia, Nombre = g.Nombre });
        }
    }
}
