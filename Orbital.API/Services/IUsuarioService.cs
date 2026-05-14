using Orbital.API.DTOs;

namespace Orbital.API.Services
{
    public interface IUsuarioService
    {
        Task<List<UsuarioResponseDto>> GetUsuarios();
        Task<Dictionary<string, List<UsuarioResponseDto>>> ObtenerUltimos3UsuariosPorRol();
        Task<UsuarioResponseDto?> GetUsuarioById(int id);
        Task UpdateUsuario(int id, UsuarioUpdateDto dto);
    }
}
