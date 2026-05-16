using Orbital.API.Data;
using Orbital.API.DTOs;
using Orbital.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Orbital.API.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(AppDbContext context, ILogger<ClienteService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ClienteResponseDto?> ObtenerPorId(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.GalaxiaOrigen)
                .FirstOrDefaultAsync(c => c.Id_Cliente == id && c.Activo);

            return cliente == null ? null : MapearDto(cliente);
        }

        public async Task<ClienteResponseDto> Actualizar(int id, ClienteUpdateDto dto, int idUsuario, string ipOrigen)
        {
            var cliente = await _context.Clientes
                .Include(c => c.GalaxiaOrigen)
                .FirstOrDefaultAsync(c => c.Id_Cliente == id && c.Activo);

            if (cliente == null)
                throw new KeyNotFoundException("Cliente no encontrado");

            var valorAnterior = JsonSerializer.Serialize(new
            {
                cliente.Nombre, cliente.Tipo_Cliente,
                cliente.Id_Galaxia_Origen, cliente.Correo, cliente.Nivel_Confianza
            });

            if (dto.Nombre != null) cliente.Nombre = dto.Nombre;
            if (dto.Tipo_Cliente != null) cliente.Tipo_Cliente = dto.Tipo_Cliente;
            if (dto.Id_Galaxia_Origen.HasValue) cliente.Id_Galaxia_Origen = dto.Id_Galaxia_Origen;
            if (dto.Correo != null) cliente.Correo = dto.Correo.Trim().ToLower();
            if (dto.Nivel_Confianza != null) cliente.Nivel_Confianza = dto.Nivel_Confianza;

            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();

            // Recargar galaxia si cambió
            await _context.Entry(cliente).Reference(c => c.GalaxiaOrigen).LoadAsync();

            await RegistrarAuditoria(
                idUsuario, "EDITAR_CLIENTE", "cliente",
                cliente.Id_Cliente, valorAnterior,
                JsonSerializer.Serialize(new
                {
                    cliente.Nombre, cliente.Tipo_Cliente,
                    cliente.Id_Galaxia_Origen, cliente.Correo, cliente.Nivel_Confianza
                }),
                ipOrigen);

            return MapearDto(cliente);
        }

        public async Task<ClienteResponseDto> AjustarCredito(int id, CreditoAjusteDto dto, int idUsuario, string ipOrigen)
        {
            var cliente = await _context.Clientes
                .Include(c => c.GalaxiaOrigen)
                .FirstOrDefaultAsync(c => c.Id_Cliente == id && c.Activo);

            if (cliente == null)
                throw new KeyNotFoundException("Cliente no encontrado");

            var creditoAnterior = cliente.Credito_Disponible;
            var creditoNuevo = creditoAnterior + dto.Monto;

            if (creditoNuevo < 0)
                throw new InvalidOperationException(
                    $"El crédito resultante sería negativo ({creditoNuevo}). Crédito actual: {creditoAnterior}");

            cliente.Credito_Disponible = creditoNuevo;
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();

            await RegistrarAuditoria(
                idUsuario,
                $"AJUSTE_CREDITO. Motivo: {dto.Motivo}",
                "cliente",
                cliente.Id_Cliente,
                creditoAnterior.ToString("F2"),
                creditoNuevo.ToString("F2"),
                ipOrigen);

            return MapearDto(cliente);
        }

        private static ClienteResponseDto MapearDto(Cliente c) => new()
        {
            Id_Cliente = c.Id_Cliente,
            Nombre = c.Nombre,
            Tipo_Cliente = c.Tipo_Cliente,
            Galaxia_Origen = c.GalaxiaOrigen?.Nombre,
            Correo = c.Correo,
            Credito_Disponible = c.Credito_Disponible,
            Nivel_Confianza = c.Nivel_Confianza,
            Fecha_Registro = c.Fecha_Registro,
            Activo = c.Activo
        };

        private async Task RegistrarAuditoria(
            int? idUsuario, string accion, string tabla,
            int idRegistro, string? valorAnterior, string? valorNuevo, string? ipOrigen)
        {
            _context.Auditorias.Add(new Auditoria
            {
                Id_Usuario = idUsuario,
                Accion = accion,
                Tabla_Afectada = tabla,
                Id_Registro_Afectado = idRegistro,
                Valor_Anterior = valorAnterior,
                Valor_Nuevo = valorNuevo,
                Timestamp_Accion = DateTime.UtcNow,
                Ip_Origen = ipOrigen,
                Resultado = "Exitoso"
            });
            await _context.SaveChangesAsync();
        }
    }
}
