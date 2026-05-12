using Orbital.API.DTOs;
using Orbital.API.Models;
using Orbital.API.Repositories;

namespace Orbital.API.Services
{
    public interface IRecursoService
    {
        Task<RecursoResponseDto> Crear(RecursoCreateDto dto);
        Task<IEnumerable<RecursoResponseDto>> ObtenerTodos();
        Task<RecursoResponseDto?> ObtenerPorId(int id);
        Task<RecursoResponseDto> Actualizar(int id, RecursoUpdateDto dto);
        Task<bool> Eliminar(int id);
    }

    public class RecursoService : IRecursoService
    {
        private readonly IRecursoRepository _repository;

        public RecursoService(IRecursoRepository repository)
        {
            _repository = repository;
        }

        public async Task<RecursoResponseDto> Crear(RecursoCreateDto dto)
        {
            var recurso = new Recurso
            {
                Nombre = dto.Nombre,
                Tipo_Recurso = dto.Tipo_Recurso,
                Unidad_Medida = dto.Unidad_Medida,
                Rareza = dto.Rareza
            };

            var creado = await _repository.Crear(recurso);
            return MapToDto(creado);
        }

        public async Task<IEnumerable<RecursoResponseDto>> ObtenerTodos()
        {
            var data = await _repository.ObtenerTodos();
            return data.Select(MapToDto);
        }

        public async Task<RecursoResponseDto?> ObtenerPorId(int id)
        {
            var recurso = await _repository.ObtenerPorId(id);
            if (recurso == null) return null;
            return MapToDto(recurso);
        }

        public async Task<RecursoResponseDto> Actualizar(int id, RecursoUpdateDto dto)
        {
            var existente = await _repository.ObtenerPorId(id);

            if (existente == null)
                throw new Exception("Recurso no encontrado");

            existente.Nombre = dto.Nombre ?? existente.Nombre;
            existente.Tipo_Recurso = dto.Tipo_Recurso ?? existente.Tipo_Recurso;
            existente.Unidad_Medida = dto.Unidad_Medida ?? existente.Unidad_Medida;
            existente.Rareza = dto.Rareza ?? existente.Rareza;

            var actualizado = await _repository.Actualizar(existente);
            return MapToDto(actualizado);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _repository.Eliminar(id);
        }

        private static RecursoResponseDto MapToDto(Recurso r)
        {
            return new RecursoResponseDto
            {
                Id_Recurso = r.Id_Recurso,
                Nombre = r.Nombre,
                Tipo_Recurso = r.Tipo_Recurso,
                Unidad_Medida = r.Unidad_Medida,
                Rareza = r.Rareza
            };
        }
    }
}