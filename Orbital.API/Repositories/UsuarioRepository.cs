using Orbital.API.Data;
using Orbital.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Orbital.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Crear(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario?> ObtenerPorEmail(string email)
        {
            return await _context.Usuarios
                .Include(x => x.Rol)
                .FirstOrDefaultAsync(x => x.Correo == email);
        }

        public async Task<List<Usuario>> ObtenerTodos()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> ObtenerPorId(Guid id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task Actualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}