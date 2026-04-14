using Orbital.API.DTOs;
using Orbital.API.Models;
using Orbital.API.Repositories;

namespace Orbital.API.Services
{
    public class PlanetasService : IPlanetasService
    {
        private readonly IPlanetasRepository _repository;

        public PlanetasService(IPlanetasRepository repository)
        {
            _repository = repository;
        }

        public async Task<PlanetaResponseDto> CrearPlaneta(PlanetaCreateDto dto)
        {
            var planeta = new Planeta
            {
                Nombre = dto.Nombre,
                Sistema_Estelar = dto.Sistema_Estelar,
                Galaxia = dto.Galaxia,
                Nivel_Tecnologico = dto.Nivel_Tecnologico,
                Atmosfera = dto.Atmosfera,
                Poblacion = dto.Poblacion,
                Nivel_Vida_Nativa = dto.Nivel_Vida_Nativa,
                Id_Estado = dto.Id_Estado,
                Id_Propietario = dto.Id_Propietario,
                Fecha_Descubrimiento = dto.Fecha_Descubrimiento,
                Coordenadas = dto.Coordenadas,
                Descripcion = dto.Descripcion,
                Activo = dto.Activo
            };

            var creado = await _repository.CrearPlaneta(planeta);
            return MapToDto(creado);
        }

        public async Task<IEnumerable<PlanetaResponseDto>> ObtenerTodosPlanetas()
        {
            var data = await _repository.ObtenerTodosPlanetas();
            return data.Select(MapToDto);
        }

        public async Task<PlanetaResponseDto> ObtenerPlanetaPorId(int id)
        {
            var planeta = await _repository.ObtenerPlanetaPorId(id);

            if (planeta == null)
                throw new Exception("Planeta no encontrado");

            return MapToDto(planeta);
        }

        public async Task<PlanetaResponseDto> ActualizarPlaneta(int id, PlanetaUpdateDto dto)
        {
            var existente = await _repository.ObtenerPlanetaPorId(id);

            if (existente == null)
                throw new Exception("Planeta no encontrado");

            existente.Nombre = dto.Nombre ?? existente.Nombre;
            existente.Sistema_Estelar = dto.Sistema_Estelar ?? existente.Sistema_Estelar;
            existente.Galaxia = dto.Galaxia ?? existente.Galaxia;

            if (dto.Nivel_Tecnologico.HasValue)
                existente.Nivel_Tecnologico = dto.Nivel_Tecnologico.Value;

            existente.Atmosfera = dto.Atmosfera ?? existente.Atmosfera;

            if (dto.Poblacion.HasValue)
                existente.Poblacion = dto.Poblacion.Value;

            existente.Nivel_Vida_Nativa = dto.Nivel_Vida_Nativa ?? existente.Nivel_Vida_Nativa;

            if (dto.Id_Estado.HasValue)
                existente.Id_Estado = dto.Id_Estado.Value;

            if (dto.Id_Propietario.HasValue)
                existente.Id_Propietario = dto.Id_Propietario.Value;

            if (dto.Fecha_Descubrimiento.HasValue)
                existente.Fecha_Descubrimiento = dto.Fecha_Descubrimiento.Value;

            existente.Coordenadas = dto.Coordenadas ?? existente.Coordenadas;
            existente.Descripcion = dto.Descripcion ?? existente.Descripcion;

            if (dto.Activo.HasValue)
                existente.Activo = dto.Activo.Value;

            var actualizado = await _repository.ActualizarPlaneta(existente);
            return MapToDto(actualizado);
        }

        public async Task<bool> EliminarPlaneta(int id)
        {
            return await _repository.EliminarPlaneta(id);
        }

        private static PlanetaResponseDto MapToDto(Planeta p)
        {
            return new PlanetaResponseDto
            {
                Id_Planeta = p.Id_Planeta,
                Nombre = p.Nombre,
                Sistema_Estelar = p.Sistema_Estelar,
                Galaxia = p.Galaxia,
                Nivel_Tecnologico = p.Nivel_Tecnologico,
                Atmosfera = p.Atmosfera,
                Poblacion = p.Poblacion,
                Nivel_Vida_Nativa = p.Nivel_Vida_Nativa,
                Id_Estado = p.Id_Estado,
                Id_Propietario = p.Id_Propietario,
                Fecha_Descubrimiento = p.Fecha_Descubrimiento,
                Coordenadas = p.Coordenadas,
                Descripcion = p.Descripcion,
                Activo = p.Activo
            };
        }
    }
}