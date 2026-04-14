using Microsoft.EntityFrameworkCore;
using Orbital.API.Models;

namespace Orbital.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // =========================
        // DBSETS
        // =========================
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Jerarquia> Jerarquias { get; set; }
        public DbSet<Planeta> Planetas { get; set; }
        public DbSet<PlanetaEstado> PlanetaEstados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // TABLE MAPPING
            // =========================
            modelBuilder.Entity<Usuario>().ToTable("usuario");
            modelBuilder.Entity<Rol>().ToTable("rol");
            modelBuilder.Entity<Jerarquia>().ToTable("jerarquia");
            modelBuilder.Entity<Planeta>().ToTable("planeta");
            modelBuilder.Entity<PlanetaEstado>().ToTable("planeta_estado");

            // =========================
            // PRIMARY KEYS
            // =========================
            modelBuilder.Entity<Usuario>()
                .HasKey(x => x.Id_Usuario);

            modelBuilder.Entity<Rol>()
                .HasKey(x => x.Id_Rol);

            modelBuilder.Entity<Jerarquia>()
                .HasKey(x => x.Id_Jerarquia);

            modelBuilder.Entity<Planeta>()
                .HasKey(x => x.Id_Planeta);

            modelBuilder.Entity<PlanetaEstado>()
                .HasKey(x => x.Id_Estado);

            // =========================
            // RELACIONES
            // =========================
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany()
                .HasForeignKey(u => u.Id_Rol)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Jerarquia)
                .WithMany()
                .HasForeignKey(u => u.Id_Jerarquia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Planeta>()
                .HasOne(p => p.Estado)
                .WithMany()
                .HasForeignKey(p => p.Id_Estado)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}