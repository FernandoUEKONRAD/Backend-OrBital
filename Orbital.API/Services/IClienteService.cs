using Orbital.API.DTOs;

namespace Orbital.API.Services
{
    public interface IClienteService
    {
        Task<ClienteResponseDto?> ObtenerPorId(int id);
        Task<ClienteResponseDto> Actualizar(int id, ClienteUpdateDto dto, int idUsuario, string ipOrigen);
        Task<ClienteResponseDto> AjustarCredito(int id, CreditoAjusteDto dto, int idUsuario, string ipOrigen);
    }
}
