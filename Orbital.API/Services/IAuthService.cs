using Orbital.API.DTOs;
using Orbital.API.Models;

namespace Orbital.API.Services
{
    public interface IAuthService
    {
        Task<ResponseLoginDto> Login(UsuarioLoginDto dto);

        Task<Usuario> Register(UsuarioCreateDto dto);
    }
}