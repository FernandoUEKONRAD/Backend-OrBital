using Orbital.API.DTOs;
using Orbital.API.Models;
using Orbital.API.Repositories;
using Orbital.API.Data;
using Microsoft.Extensions.Logging;

namespace Orbital.API.Services
{
    public class PlanetasService : IPlanetasService
    {
        private readonly IPlanetasRepository _repository;
        private readonly AppDbContext _context;
        private readonly ILogger<PlanetasService> _logger;

        public PlanetasService(IPlanetasRepository repository, AppDbContext context, ILogger<PlanetasService> logger)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        public async Task<PlanetaResponseDto> CrearPlaneta(PlanetaCreateDto dto)
        {
            var planeta = new Planeta
            {
                Nombre              = dto.Nombre,
                Id_Galaxia          = dto.Id_Galaxia,
                Nivel_Tecnologico   = dto.Nivel_Tecnologico,
                Id_Atmosfera        = dto.Id_Atmosfera,
                Poblacion           = dto.Poblacion,
                Nivel_Vida_Planeta  = dto.Nivel_Vida_Planeta,
                Id_Estado           = dto.Id_Estado,
                Id_Propietario      = dto.Id_Propietario,
                Conquistado_Por     = dto.Conquistado_Por,
                Fecha_Descubrimiento = dto.Fecha_Descubrimiento,
                Descripcion         = dto.Descripcion,
                Color1              = dto.Color1,
                Color2              = dto.Color2,
                Color3              = dto.Color3,
                Activo              = dto.Activo
            };

            if (dto.CoordenadaX.HasValue && dto.CoordenadaY.HasValue && dto.CoordenadaZ.HasValue)
            {
                planeta.Coordenadas = new CoordenadasPlaneta
                {
                    Coordenada_X = dto.CoordenadaX.Value,
                    Coordenada_Y = dto.CoordenadaY.Value,
                    Coordenada_Z = dto.CoordenadaZ.Value
                };
            }

            var creado = await _repository.CrearPlaneta(planeta);
            return MapToSimpleDto(creado);
        }

        public async Task<IEnumerable<PlanetaListItemDto>> ObtenerTodosPlanetas(
            int? idPlaneta = null,
            string? nombre = null,
            int? idAtmosfera = null,
            NivelTecnologico? nivelTecnologico = null,
            long? poblacionMin = null,
            long? poblacionMax = null,
            int? idEstado = null,
            string? tipoRecurso = null)
        {
            var data = await _repository.ObtenerTodosPlanetas(
                idPlaneta, nombre, idAtmosfera, nivelTecnologico,
                poblacionMin, poblacionMax, idEstado, tipoRecurso);

            return data.Select(MapToListItemDto);
        }

        public async Task<IEnumerable<PlanetaGalaxiaItemDto>> ObtenerPlanetasPorGalaxia(int galaxiaId)
        {
            var data = await _repository.ObtenerPlanetasPorGalaxia(galaxiaId);
            return data.Select(p => new PlanetaGalaxiaItemDto
            {
                Id_Planeta  = p.Id_Planeta,
                Nombre      = p.Nombre,
                Coordenadas = MapCoordenadas(p.Coordenadas),
                Color1      = p.Color1,
                Color2      = p.Color2,
                Color3      = p.Color3
            });
        }

        public async Task<PlanetaDetalleDto?> ObtenerPlanetaPorId(int id)
        {
            var planeta = await _repository.ObtenerPlanetaPorId(id);
            if (planeta == null) return null;
            return MapToDetalleDto(planeta);
        }

        public async Task<PlanetaResponseDto> ActualizarPlaneta(int id, PlanetaUpdateDto dto)
        {
            var existente = await _repository.ObtenerPlanetaPorId(id);
            if (existente == null)
                throw new KeyNotFoundException("Planeta no encontrado");

            if (dto.Nombre              != null) existente.Nombre             = dto.Nombre;
            if (dto.Id_Galaxia.HasValue)          existente.Id_Galaxia         = dto.Id_Galaxia;
            if (dto.Nivel_Tecnologico.HasValue)   existente.Nivel_Tecnologico  = dto.Nivel_Tecnologico.Value;
            if (dto.Id_Atmosfera.HasValue)        existente.Id_Atmosfera       = dto.Id_Atmosfera;
            if (dto.Poblacion.HasValue)           existente.Poblacion          = dto.Poblacion;
            if (dto.Nivel_Vida_Planeta  != null) existente.Nivel_Vida_Planeta = dto.Nivel_Vida_Planeta;
            if (dto.Id_Estado.HasValue)           existente.Id_Estado          = dto.Id_Estado.Value;
            if (dto.Id_Propietario.HasValue)      existente.Id_Propietario     = dto.Id_Propietario;
            if (dto.Conquistado_Por    != null)   existente.Conquistado_Por    = dto.Conquistado_Por;
            if (dto.Fecha_Descubrimiento.HasValue) existente.Fecha_Descubrimiento = dto.Fecha_Descubrimiento.Value;
            if (dto.Descripcion        != null)   existente.Descripcion        = dto.Descripcion;
            if (dto.Color1             != null)   existente.Color1             = dto.Color1;
            if (dto.Color2             != null)   existente.Color2             = dto.Color2;
            if (dto.Color3             != null)   existente.Color3             = dto.Color3;
            if (dto.Activo.HasValue)              existente.Activo             = dto.Activo.Value;

            if (dto.CoordenadaX.HasValue && dto.CoordenadaY.HasValue && dto.CoordenadaZ.HasValue)
            {
                if (existente.Coordenadas != null)
                {
                    existente.Coordenadas.Coordenada_X = dto.CoordenadaX.Value;
                    existente.Coordenadas.Coordenada_Y = dto.CoordenadaY.Value;
                    existente.Coordenadas.Coordenada_Z = dto.CoordenadaZ.Value;
                }
                else
                {
                    existente.Coordenadas = new CoordenadasPlaneta
                    {
                        Id_Planeta   = existente.Id_Planeta,
                        Coordenada_X = dto.CoordenadaX.Value,
                        Coordenada_Y = dto.CoordenadaY.Value,
                        Coordenada_Z = dto.CoordenadaZ.Value
                    };
                }
            }

            var actualizado = await _repository.ActualizarPlaneta(existente);
            return MapToSimpleDto(actualizado);
        }

        public async Task<bool> DesactivarPlaneta(int id, int idUsuario, DesactivarPlanetaDto dto)
        {
            _logger.LogInformation("Iniciando desactivación del planeta {IdPlaneta} por usuario {IdUsuario}", id, idUsuario);

            // Obtener planeta
            var planeta = await _repository.ObtenerPlanetaPorId(id);
            if (planeta == null)
            {
                _logger.LogWarning("Planeta {IdPlaneta} no encontrado", id);
                throw new KeyNotFoundException("Planeta no encontrado");
            }

            // Validar procesos activos asociados
            var procesosActivos = new List<string>();

            // 1. Validar misiones activas (estados: 1=Pendiente, 2=En Preparación, 3=En Curso)
            var misionesActivas = _context.Misiones
                .Where(m => m.Id_Planeta == id && 
                           (m.Id_Estado_Mision == 1 || m.Id_Estado_Mision == 2 || m.Id_Estado_Mision == 3))
                .Count();
            if (misionesActivas > 0)
                procesosActivos.Add($"Misiones activas: {misionesActivas}");

            // 2. Validar valoraciones pendientes
            var valoracionesPendientes = _context.PlanetaValoraciones
                .Where(v => v.Id_Planeta == id && v.Estado_Valoracion == "Pendiente")
                .Count();
            if (valoracionesPendientes > 0)
                procesosActivos.Add($"Valoraciones pendientes: {valoracionesPendientes}");

            // 3. Validar publicaciones en mercado activas
            var publicacionesActivas = _context.MercadoPlanetas
                .Where(m => m.Id_Planeta == id && m.Activo)
                .Count();
            if (publicacionesActivas > 0)
                procesosActivos.Add($"Publicaciones en mercado: {publicacionesActivas}");

            // 4. Validar amenazas activas
            var amenazasActivas = _context.AmenazasDeteccion
                .Where(a => a.Id_Planeta == id && 
                           (a.Estado_Amenaza == "Activa" || a.Estado_Amenaza == "En Seguimiento" || a.Estado_Amenaza == "Escalada"))
                .Count();
            if (amenazasActivas > 0)
                procesosActivos.Add($"Amenazas detectadas: {amenazasActivas}");

            // 5. Validar transacciones activas del planeta (a través del mercado)
            var transaccionesActivas = _context.Transacciones
                .Where(t => _context.MercadoPlanetas.Where(m => m.Id_Planeta == id).Select(m => m.Id_Publicacion).Contains(t.Id_Publicacion) &&
                           (t.Estado_Transaccion == "Pendiente" || t.Estado_Transaccion == "En Disputa"))
                .Count();
            if (transaccionesActivas > 0)
                procesosActivos.Add($"Transacciones activas: {transaccionesActivas}");

            // Bloquear si hay procesos activos
            if (procesosActivos.Count > 0)
            {
                var mensaje = $"No se puede desactivar el planeta. Procesos activos: {string.Join("; ", procesosActivos)}";
                _logger.LogWarning("Intento de desactivación bloqueado para planeta {IdPlaneta}: {Procesos}", id, mensaje);
                throw new InvalidOperationException(mensaje);
            }

            // Obtener usuario que realiza la acción
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null)
            {
                _logger.LogWarning("Usuario {IdUsuario} no encontrado", idUsuario);
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            // Marcar planeta como inactivo
            planeta.Activo = false;
            await _repository.ActualizarPlaneta(planeta);

            // Registrar en auditoría
            var auditoria = new Auditoria
            {
                Id_Usuario = idUsuario,
                Accion = "DESACTIVAR_PLANETA",
                Tabla_Afectada = "planeta",
                Id_Registro_Afectado = planeta.Id_Planeta,
                Valor_Anterior = $"Activo: true, Nombre: {planeta.Nombre}",
                Valor_Nuevo = $"Activo: false, Justificación: {dto.Justificacion}",
                Timestamp_Accion = DateTime.UtcNow,
                Resultado = "Exitoso"
            };

            _context.Auditorias.Add(auditoria);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Planeta {IdPlaneta} desactivado exitosamente. Auditoría registrada: {IdAuditoria}", 
                id, auditoria.Id_Auditoria);

            return true;
        }

        // =========================
        // HELPERS
        // =========================

        private static string ResolverDificultadConquista(string nivelVida) =>
            nivelVida.ToLower() switch
            {
                "sin vida"  => "Sin resistencia",
                "primitiva" => "Muy baja",
                "bajo"      => "Baja",
                "medio"     => "Media",
                "alto"      => "Alta",
                "avanzado"  => "Muy alta",
                _           => "Desconocida"
            };

        private static CoordenadasDto? MapCoordenadas(CoordenadasPlaneta? c) =>
            c == null ? null : new CoordenadasDto { X = c.Coordenada_X, Y = c.Coordenada_Y, Z = c.Coordenada_Z };

        private static List<RecursoPlanetaDto> MapRecursos(ICollection<RecursoPlaneta> recursos) =>
            recursos
                .Where(rp => rp.Recurso != null)
                .Select(rp => new RecursoPlanetaDto
                {
                    Id_Recurso        = rp.Id_Recurso,
                    Nombre            = rp.Recurso!.Nombre,
                    Tipo_Recurso      = rp.Recurso.Tipo_Recurso,
                    Unidad_Medida     = rp.Recurso.Unidad_Medida,
                    Rareza            = rp.Recurso.Rareza,
                    Cantidad_Estimada = rp.Cantidad_Estimada,
                    Valor_Unitario    = rp.Valor_Unitario,
                    Extraible         = rp.Extraible
                })
                .ToList();

        private static PlanetaListItemDto MapToListItemDto(Planeta p) => new()
        {
            Id_Planeta             = p.Id_Planeta,
            Nombre                 = p.Nombre,
            Coordenadas            = MapCoordenadas(p.Coordenadas),
            TipoAtmosfera          = p.AtmosferaNav?.Nombre,
            NivelTecnologico       = p.Nivel_Tecnologico.ToString(),
            Poblacion              = p.Poblacion,
            Estado                 = p.Estado?.Nombre ?? p.Id_Estado.ToString(),
            Recursos               = MapRecursos(p.Recursos),
            NivelDificultadConquista = ResolverDificultadConquista(p.Nivel_Vida_Planeta)
        };

        private static PlanetaDetalleDto MapToDetalleDto(Planeta p) => new()
        {
            Id_Planeta           = p.Id_Planeta,
            Nombre               = p.Nombre,
            Coordenadas          = MapCoordenadas(p.Coordenadas),
            TipoAtmosfera        = p.AtmosferaNav?.Nombre,
            NivelTecnologico     = p.Nivel_Tecnologico.ToString(),
            Poblacion            = p.Poblacion,
            Estado               = p.Estado?.Nombre ?? p.Id_Estado.ToString(),
            Recursos             = MapRecursos(p.Recursos),
            NivelVidaPlaneta     = p.Nivel_Vida_Planeta,
            Galaxia              = p.GalaxiaNav?.Nombre,
            Descripcion          = p.Descripcion,
            Fecha_Descubrimiento = p.Fecha_Descubrimiento,
            Misiones             = p.Misiones.Select(m => new MisionResumenDto
            {
                Id_Mision          = m.Id_Mision,
                Nombre_Mision      = m.Nombre_Mision,
                Tipo_Mision        = m.Tipo_Mision,
                Id_Estado_Mision   = m.Id_Estado_Mision,
                Porcentaje_Avance  = m.Porcentaje_Avance,
                Fecha_Inicio       = m.Fecha_Inicio,
                Fecha_Fin_Estimada = m.Fecha_Fin_Estimada
            }).ToList(),
            Color1          = p.Color1,
            Color2          = p.Color2,
            Color3          = p.Color3,
            Conquistado_Por = p.Conquistado_Por,
            Activo          = p.Activo
        };

        private static PlanetaResponseDto MapToSimpleDto(Planeta p) => new()
        {
            Id_Planeta           = p.Id_Planeta,
            Nombre               = p.Nombre,
            Id_Galaxia           = p.Id_Galaxia,
            NivelTecnologico     = p.Nivel_Tecnologico.ToString(),
            Id_Atmosfera         = p.Id_Atmosfera,
            Poblacion            = p.Poblacion,
            Nivel_Vida_Planeta   = p.Nivel_Vida_Planeta,
            Id_Estado            = p.Id_Estado,
            Id_Propietario       = p.Id_Propietario,
            Conquistado_Por      = p.Conquistado_Por,
            Fecha_Descubrimiento = p.Fecha_Descubrimiento,
            Descripcion          = p.Descripcion,
            Color1               = p.Color1,
            Color2               = p.Color2,
            Color3               = p.Color3,
            Activo               = p.Activo
        };
    }
}
