using Orbital.API.DTOs;
using Orbital.API.Models;
using Orbital.API.Repositories;

namespace Orbital.API.Services
{
    public interface IPlanetasService
    {
        Task<PlanetaResponseDto> CrearPlaneta(PlanetaCreateDto dto);
        Task<List<PlanetaResponseDto>> ObtenerTodosPlanetas();
        Task<PlanetaResponseDto> ObtenerPlanetaPorId(int id);
        Task<PlanetaResponseDto> ActualizarPlaneta(int id, PlanetaUpdateDto dto);
        Task<bool> EliminarPlaneta(int id);
    }

    public class PlanetasService : IPlanetasService
    {
        private readonly IPlanetasRepository _repository;

        public PlanetasService(IPlanetasRepository repository)
        {
            _repository = repository;
        }

        public async Task<PlanetaResponseDto> CrearPlaneta(PlanetaCreateDto dto)
        {
            try
            {
                // Validar datos obligatorios
                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    throw new ArgumentException("El nombre del planeta es obligatorio");

                if (string.IsNullOrWhiteSpace(dto.Descripcion))
                    throw new ArgumentException("La descripción del planeta es obligatoria");

                if (string.IsNullOrWhiteSpace(dto.Tipo))
                    throw new ArgumentException("El tipo de planeta es obligatorio");

                if (dto.Diametro <= 0)
                    throw new ArgumentException("El diámetro debe ser mayor a 0");

                if (dto.DistanciaAlSol <= 0)
                    throw new ArgumentException("La distancia al sol debe ser mayor a 0");

                if (dto.TiempoOrbita <= 0)
                    throw new ArgumentException("El tiempo de órbita debe ser mayor a 0");

                // Crear planeta con valores por defecto
                var planeta = new Planeta
                {
                    Nombre = dto.Nombre.Trim(),
                    Descripcion = dto.Descripcion.Trim(),
                    Diametro = dto.Diametro,
                    Tipo = dto.Tipo.Trim(),
                    DistanciaAlSol = dto.DistanciaAlSol,
                    TiempoOrbita = dto.TiempoOrbita,
                    TieneAtmosfera = dto.TieneAtmosfera,
                    NumeroLunas = dto.NumeroLunas,
                    Habitable = dto.Habitable,
                    Estado = "Activo", // Estado por defecto
                    FechaRegistro = DateTime.UtcNow
                };

                var planetaCreado = await _repository.CrearPlaneta(planeta);
                return MapearAResponseDto(planetaCreado);
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Error de validación: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el planeta", ex);
            }
        }

        public async Task<List<PlanetaResponseDto>> ObtenerTodosPlanetas()
        {
            try
            {
                var planetas = await _repository.ObtenerTodosPlanetas();
                return planetas.Select(MapearAResponseDto).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los planetas", ex);
            }
        }

        public async Task<PlanetaResponseDto> ObtenerPlanetaPorId(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor a 0");

                var planeta = await _repository.ObtenerPlanetaPorId(id);
                return MapearAResponseDto(planeta);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el planeta con ID {id}", ex);
            }
        }

        public async Task<PlanetaResponseDto> ActualizarPlaneta(int id, PlanetaUpdateDto dto)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor a 0");

                // Validar que al menos un campo sea actualizado
                if (dto == null || (string.IsNullOrEmpty(dto.Nombre) && 
                    dto.Diametro == null && 
                    string.IsNullOrEmpty(dto.Tipo) &&
                    string.IsNullOrEmpty(dto.Estado)))
                {
                    throw new ArgumentException("Debe proporcionar al menos un campo para actualizar");
                }

                // Obtener planeta actual
                var planetaActual = await _repository.ObtenerPlanetaPorId(id);

                // Crear objeto de actualización
                var planetaActualizado = new Planeta
                {
                    Id = planetaActual.Id,
                    Nombre = string.IsNullOrWhiteSpace(dto.Nombre) ? planetaActual.Nombre : dto.Nombre.Trim(),
                    Descripcion = string.IsNullOrWhiteSpace(dto.Descripcion) ? planetaActual.Descripcion : dto.Descripcion.Trim(),
                    Diametro = dto.Diametro ?? planetaActual.Diametro,
                    Tipo = string.IsNullOrWhiteSpace(dto.Tipo) ? planetaActual.Tipo : dto.Tipo.Trim(),
                    DistanciaAlSol = dto.DistanciaAlSol ?? planetaActual.DistanciaAlSol,
                    TiempoOrbita = dto.TiempoOrbita ?? planetaActual.TiempoOrbita,
                    TieneAtmosfera = dto.TieneAtmosfera ?? planetaActual.TieneAtmosfera,
                    NumeroLunas = dto.NumeroLunas ?? planetaActual.NumeroLunas,
                    Habitable = dto.Habitable ?? planetaActual.Habitable,
                    Estado = string.IsNullOrWhiteSpace(dto.Estado) ? planetaActual.Estado : dto.Estado.Trim(),
                    FechaRegistro = planetaActual.FechaRegistro,
                    FechaActualizacion = DateTime.UtcNow
                };

                var planetaActualizadoEnBd = await _repository.ActualizarPlaneta(id, planetaActualizado);
                return MapearAResponseDto(planetaActualizadoEnBd);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el planeta con ID {id}", ex);
            }
        }

        public async Task<bool> EliminarPlaneta(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor a 0");

                // Verificar que el planeta existe antes de eliminar
                await _repository.ObtenerPlanetaPorId(id);
                return await _repository.EliminarPlaneta(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el planeta con ID {id}", ex);
            }
        }

        private PlanetaResponseDto MapearAResponseDto(Planeta planeta)
        {
            return new PlanetaResponseDto
            {
                Id = planeta.Id,
                Nombre = planeta.Nombre,
                Descripcion = planeta.Descripcion,
                Diametro = planeta.Diametro,
                Tipo = planeta.Tipo,
                DistanciaAlSol = planeta.DistanciaAlSol,
                TiempoOrbita = planeta.TiempoOrbita,
                TieneAtmosfera = planeta.TieneAtmosfera,
                NumeroLunas = planeta.NumeroLunas,
                Habitable = planeta.Habitable,
                Estado = planeta.Estado,
                FechaRegistro = planeta.FechaRegistro,
                FechaActualizacion = planeta.FechaActualizacion
            };
        }
    }
}
