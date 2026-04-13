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

        // ---------------- CREATE ----------------
        public async Task<PlanetaResponseDto> CrearPlaneta(PlanetaCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio");

            var planeta = new Planeta
            {
                Nombre = dto.Nombre.Trim(),
                Sistema_Estelar = dto.Sistema_Estelar,
                Galaxia = dto.Galaxia,
                Nivel_Tecnologico = dto.Nivel_Tecnologico,
                Atmosfera = dto.Atmosfera,
                Poblacion = dto.Poblacion ?? 0,
                Nivel_Vida_Nativa = dto.Nivel_Vida_Nativa ?? "Bajo",
                Id_Estado = dto.Id_Estado,
                Id_Propietario = dto.Id_Propietario,
                Fecha_Descubrimiento = DateTime.SpecifyKind(dto.Fecha_Descubrimiento, DateTimeKind.Utc),
                Coordenadas = string.IsNullOrWhiteSpace(dto.Coordenadas) ? null : dto.Coordenadas,
                Descripcion = dto.Descripcion,
                Activo = true
            };

            var creado = await _repository.CrearPlaneta(planeta);
            return Mapear(creado);
        }

        // ---------------- GET ALL ----------------
        public async Task<List<PlanetaResponseDto>> ObtenerTodosPlanetas()
        {
            var lista = await _repository.ObtenerTodosPlanetas();
            return lista.Select(Mapear).ToList();
        }

        // ---------------- GET BY ID ----------------
        public async Task<PlanetaResponseDto> ObtenerPlanetaPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido");

            var planeta = await _repository.ObtenerPlanetaPorId(id);
            return Mapear(planeta);
        }

        // ---------------- UPDATE ----------------
        public async Task<PlanetaResponseDto> ActualizarPlaneta(int id, PlanetaUpdateDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido");

            var actual = await _repository.ObtenerPlanetaPorId(id);

            var actualizado = new Planeta
            {
                Id_Planeta = actual.Id_Planeta,

                Nombre = dto.Nombre ?? actual.Nombre,
                Sistema_Estelar = dto.Sistema_Estelar ?? actual.Sistema_Estelar,
                Galaxia = dto.Galaxia ?? actual.Galaxia,
                Nivel_Tecnologico = dto.Nivel_Tecnologico ?? actual.Nivel_Tecnologico,
                Atmosfera = dto.Atmosfera ?? actual.Atmosfera,
                Poblacion = dto.Poblacion ?? actual.Poblacion,
                Nivel_Vida_Nativa = dto.Nivel_Vida_Nativa ?? actual.Nivel_Vida_Nativa,
                Id_Estado = dto.Id_Estado ?? actual.Id_Estado,
                Id_Propietario = dto.Id_Propietario ?? actual.Id_Propietario,
                Fecha_Descubrimiento = dto.Fecha_Descubrimiento ?? actual.Fecha_Descubrimiento,
                Coordenadas = dto.Coordenadas ?? actual.Coordenadas,
                Descripcion = dto.Descripcion ?? actual.Descripcion,
                Activo = dto.Activo ?? actual.Activo
            };

            var result = await _repository.ActualizarPlaneta(id, actualizado);
            return Mapear(result);
        }

        // ---------------- DELETE ----------------
        public async Task<bool> EliminarPlaneta(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido");

            return await _repository.EliminarPlaneta(id);
        }

        // ---------------- MAPPER ----------------
        private PlanetaResponseDto Mapear(Planeta p)
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