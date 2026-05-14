using Orbital.API.DTOs;

namespace Orbital.API.Services
{
    public interface IPlanetasService
    {
        Task<IEnumerable<PlanetaListItemDto>> ObtenerTodosPlanetas(
            int? idPlaneta = null,
            string? nombre = null,
            int? idAtmosfera = null,
            NivelTecnologico? nivelTecnologico = null,
            long? poblacionMin = null,
            long? poblacionMax = null,
            int? idEstado = null,
            string? tipoRecurso = null);

        Task<IEnumerable<PlanetaGalaxiaItemDto>> ObtenerPlanetasPorGalaxia(int galaxiaId);

        Task<PlanetaDetalleDto?> ObtenerPlanetaPorId(int id);

        Task<PlanetaResponseDto> CrearPlaneta(PlanetaCreateDto dto);

        Task<PlanetaResponseDto> ActualizarPlaneta(int id, PlanetaUpdateDto dto);

        Task<bool> DesactivarPlaneta(int id, int idUsuario, DesactivarPlanetaDto dto);
    }
}
