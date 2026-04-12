using Orbital.API.DTOs;

namespace Orbital.API.Services
{
    public interface IUsuarioService
    {
        Task<List<UsuarioResponseDto>> GetUsuarios();
    }
}