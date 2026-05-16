using Orbital.API.DTOs;

namespace Orbital.API.Services
{
    public interface IMercadoService
    {
        Task<List<MercadoListItemDto>> ListarPlanetasEnVenta(
            decimal? precioMin, decimal? precioMax, string? clase, int? galaxiaId);
        Task<MercadoDetalleDto?> ObtenerDetalle(int id);
        Task<MercadoListItemDto> PublicarPlaneta(PublicarPlanetaDto dto, int idUsuario, string ipOrigen);
        Task<MercadoListItemDto> EditarPublicacion(int id, EditarPublicacionDto dto, int idUsuario, string ipOrigen);
        Task RetirarPlaneta(int id, RetirarMercadoDto dto, int idUsuario, string ipOrigen);
    }
}
