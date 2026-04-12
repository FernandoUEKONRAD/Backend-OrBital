using Orbital.API.Models;

namespace Orbital.API.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByEmail(string email);

        Task Add(Usuario usuario);

        Task<List<Usuario>> GetAll();
    }
}