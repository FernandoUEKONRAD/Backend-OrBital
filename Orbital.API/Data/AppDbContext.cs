using Microsoft.EntityFrameworkCore;
using Orbital.API.Models;

namespace Orbital.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Jerarquia> Jerarquias { get; set; }
        public DbSet<Planeta> Planetas { get; set; }
        public DbSet<EstadoPlaneta> EstadosPlaneta { get; set; }
    }
}
