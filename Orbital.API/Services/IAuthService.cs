using Orbital.API.DTOs;
using Orbital.API.Models;

namespace Orbital.API.Services
{
    public interface IAuthService
    {
        Task<UsuarioResponseDto> Login(UsuarioLoginDto dto);

        Task<Usuario> Register(UsuarioCreateDto dto);
    }
}