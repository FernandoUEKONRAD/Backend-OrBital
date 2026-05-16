using Orbital.API.DTOs;

namespace Orbital.API.Services
{
    public interface IClienteAuthService
    {
        Task<ClienteLoginResponseDto?> Login(ClienteLoginDto dto);
        Task<ClienteResponseDto> Registrar(ClienteRegistroDto dto);
    }
}
