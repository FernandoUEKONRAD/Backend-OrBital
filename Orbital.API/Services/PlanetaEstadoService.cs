using Orbital.API.DTOs;
using Orbital.API.Models;
using Orbital.API.Repositories;

namespace Orbital.API.Services
{
    public class PlanetaEstadoService
    {
        private readonly IPlanetaEstadoRepository _repository;

        public PlanetaEstadoService(IPlanetaEstadoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PlanetaEstadoResponseDto>> ObtenerEstados()
        {
            var estados = await _repository.ObtenerEstados();

            return estados.Select(e => new PlanetaEstadoResponseDto
            {
                Id_Estado = e.Id_Estado,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion
            });
        }

        public async Task<PlanetaEstadoResponseDto> ObtenerEstadoPorId(int id)
        {
            var estado = await _repository.ObtenerEstadoPorId(id);

            if (estado == null)
                throw new Exception("Estado no encontrado");

            return new PlanetaEstadoResponseDto
            {
                Id_Estado = estado.Id_Estado,
                Nombre = estado.Nombre,
                Descripcion = estado.Descripcion
            };
        }

        public async Task<PlanetaEstadoResponseDto> CrearEstado(PlanetaEstadoCreateDto dto)
        {
            var estado = new PlanetaEstado
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            var creado = await _repository.CrearEstado(estado);

            return new PlanetaEstadoResponseDto
            {
                Id_Estado = creado.Id_Estado,
                Nombre = creado.Nombre,
                Descripcion = creado.Descripcion
            };
        }

        public async Task<PlanetaEstadoResponseDto> ActualizarEstado(int id, PlanetaEstadoUpdateDto dto)
        {
            var estado = await _repository.ObtenerEstadoPorId(id);

            if (estado == null)
                throw new Exception("Estado no encontrado");

            estado.Nombre = dto.Nombre ?? estado.Nombre;
            estado.Descripcion = dto.Descripcion ?? estado.Descripcion;

            var actualizado = await _repository.ActualizarEstado(estado);

            return new PlanetaEstadoResponseDto
            {
                Id_Estado = actualizado.Id_Estado,
                Nombre = actualizado.Nombre,
                Descripcion = actualizado.Descripcion
            };
        }

        public async Task<bool> EliminarEstado(int id)
        {
            return await _repository.EliminarEstado(id);
        }
    }
}