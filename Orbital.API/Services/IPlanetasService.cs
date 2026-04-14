using Orbital.API.DTOs;

namespace Orbital.API.Services
{
    public interface IPlanetasService
    {
        Task<IEnumerable<PlanetaResponseDto>> ObtenerTodosPlanetas();

        Task<PlanetaResponseDto> ObtenerPlanetaPorId(int id);

        Task<PlanetaResponseDto> CrearPlaneta(PlanetaCreateDto dto);

        Task<PlanetaResponseDto> ActualizarPlaneta(int id, PlanetaUpdateDto dto);

        Task<bool> EliminarPlaneta(int id);
    }
}