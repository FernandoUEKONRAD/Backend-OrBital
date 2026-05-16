using Orbital.API.DTOs;
using Orbital.API.Models;
using Orbital.API.Repositories;

namespace Orbital.API.Services
{
    public interface IRecursoPlanetarioService
    {
        Task<RecursoPlanetarioResponseDto> Crear(RecursoPlanetarioCreateDto dto);
        Task<IEnumerable<RecursoPlanetarioResponseDto>> ObtenerPorPlaneta(int idPlaneta);
        Task<RecursoPlanetarioResponseDto?> ObtenerPorId(int id);
        Task<RecursoPlanetarioResponseDto> Actualizar(int id, RecursoPlanetarioUpdateDto dto);
        Task<bool> Eliminar(int id);
    }

    public class RecursoPlanetarioService : IRecursoPlanetarioService
    {
        private readonly IRecursoPlanetarioRepository _repository;

        public RecursoPlanetarioService(IRecursoPlanetarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<RecursoPlanetarioResponseDto> Crear(RecursoPlanetarioCreateDto dto)
        {
            var rp = new RecursoPlaneta
            {
                Id_Planeta = dto.Id_Planeta,
                Id_Recurso = dto.Id_Recurso,
                Cantidad_Estimada = dto.Cantidad_Estimada,
                Valor_Unitario = dto.Valor_Unitario,
                Extraible = dto.Extraible,
                Fecha_Registro = DateTime.Now
            };

            var creado = await _repository.Crear(rp);
            var conRecurso = await _repository.ObtenerPorId(creado.Id_Recurso_Planeta);
            return MapToDto(conRecurso!);
        }

        public async Task<IEnumerable<RecursoPlanetarioResponseDto>> ObtenerPorPlaneta(int idPlaneta)
        {
            var data = await _repository.ObtenerPorPlaneta(idPlaneta);
            return data.Select(MapToDto);
        }

        public async Task<RecursoPlanetarioResponseDto?> ObtenerPorId(int id)
        {
            var rp = await _repository.ObtenerPorId(id);
            if (rp == null) return null;
            return MapToDto(rp);
        }

        public async Task<RecursoPlanetarioResponseDto> Actualizar(int id, RecursoPlanetarioUpdateDto dto)
        {
            var existente = await _repository.ObtenerPorId(id);

            if (existente == null)
                throw new Exception("Recurso planetario no encontrado");

            if (dto.Cantidad_Estimada.HasValue)
                existente.Cantidad_Estimada = dto.Cantidad_Estimada.Value;

            if (dto.Valor_Unitario.HasValue)
                existente.Valor_Unitario = dto.Valor_Unitario.Value;

            if (dto.Extraible.HasValue)
                existente.Extraible = dto.Extraible.Value;

            var actualizado = await _repository.Actualizar(existente);
            return MapToDto(actualizado);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _repository.Eliminar(id);
        }

        private static RecursoPlanetarioResponseDto MapToDto(RecursoPlaneta rp)
        {
            return new RecursoPlanetarioResponseDto
            {
                Id_Recurso_Planeta = rp.Id_Recurso_Planeta,
                Id_Planeta = rp.Id_Planeta,
                Id_Recurso = rp.Id_Recurso,
                Nombre_Recurso = rp.Recurso?.Nombre ?? "",
                Tipo_Recurso = rp.Recurso?.Tipo_Recurso ?? "",
                Unidad_Medida = rp.Recurso?.Unidad_Medida ?? "",
                Rareza = rp.Recurso?.Rareza ?? "",
                Cantidad_Estimada = rp.Cantidad_Estimada,
                Valor_Unitario = rp.Valor_Unitario,
                Extraible = rp.Extraible,
                Fecha_Registro = rp.Fecha_Registro
            };
        }
    }
}
