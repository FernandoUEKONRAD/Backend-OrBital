using Orbital.API.DTOs;

namespace Orbital.API.Services
{
    public interface IUsuarioService
    {
        Task<List<UsuarioResponseDto>> GetUsuarios();
        Task<UsuarioResponseDto?> GetUsuarioById(int id);
        Task UpdateUsuario(int id, UsuarioUpdateDto dto);
    }
}
