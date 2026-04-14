using Orbital.API.Models;

namespace Orbital.API.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Crear(Usuario usuario);
        Task<Usuario?> ObtenerPorEmail(string email);
        Task<List<Usuario>> ObtenerTodos();
        Task<Usuario?> ObtenerPorId(Guid id);
        Task Actualizar(Usuario usuario);
    }
}