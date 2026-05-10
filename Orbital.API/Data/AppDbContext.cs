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
        public DbSet<PlanetaValoracion> PlanetaValoraciones { get; set; }
        public DbSet<RecursoPlanetario> RecursosPlanetarios { get; set; }
        public DbSet<Galaxia> Galaxias { get; set; }
        public DbSet<TipoAtmosfera> TiposAtmosfera { get; set; }

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
            modelBuilder.Entity<PlanetaEstado>().ToTable("estado_planeta");
            modelBuilder.Entity<PlanetaValoracion>().ToTable("planeta_valoracion");
            modelBuilder.Entity<RecursoPlanetario>().ToTable("recurso_planetario");
            modelBuilder.Entity<Galaxia>().ToTable("galaxia");
            modelBuilder.Entity<TipoAtmosfera>().ToTable("tipo_atmosfera");

            // Column mappings to match existing DB schema (snake_case names)
            modelBuilder.Entity<TipoAtmosfera>(b =>
            {
                b.Property(p => p.Id_Atm).HasColumnName("id_atmosfera");
                b.Property(p => p.Nombre).HasColumnName("nombre");
                b.Property(p => p.Descripcion).HasColumnName("descripcion");
                // DB script for tipo_atmosfera does not include an 'activo' column;
                // ignore the Activo property to avoid querying a non-existing column.
                b.Ignore(p => p.Activo);
            });

            modelBuilder.Entity<Galaxia>(b =>
            {
                b.Property(p => p.Id_Galaxia).HasColumnName("id_galaxia");
                b.Property(p => p.Nombre).HasColumnName("nombre");
                b.Property(p => p.Descripcion).HasColumnName("descripcion");
                // The galaxia table in the provided script does not include 'activo'; ignore it.
                b.Ignore(p => p.Activo);
            });

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

            modelBuilder.Entity<PlanetaValoracion>()
                .HasKey(x => x.Id_Valoracion);

            modelBuilder.Entity<RecursoPlanetario>()
                .HasKey(x => x.Id_Recurso);

            modelBuilder.Entity<Galaxia>()
                .HasKey(x => x.Id_Galaxia);

            modelBuilder.Entity<TipoAtmosfera>()
                .HasKey(x => x.Id_Atm);

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

            modelBuilder.Entity<PlanetaValoracion>()
                .HasOne(pv => pv.Planeta)
                .WithMany()
                .HasForeignKey(pv => pv.Id_Planeta)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanetaValoracion>()
                .HasOne(pv => pv.Analista)
                .WithMany()
                .HasForeignKey(pv => pv.Id_Analista)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanetaValoracion>()
                .HasOne(pv => pv.AprobadoPor)
                .WithMany()
                .HasForeignKey(pv => pv.Aprobado_Por)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RecursoPlanetario>()
                .HasOne(rp => rp.Planeta)
                .WithMany()
                .HasForeignKey(rp => rp.Id_Planeta)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanetaValoracion>()
                .HasOne(pv => pv.Planeta)
                .WithMany()
                .HasForeignKey(pv => pv.Id_Planeta)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanetaValoracion>()
                .HasOne(pv => pv.Analista)
                .WithMany()
                .HasForeignKey(pv => pv.Id_Analista)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanetaValoracion>()
                .HasOne(pv => pv.AprobadoPor)
                .WithMany()
                .HasForeignKey(pv => pv.Aprobado_Por)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RecursoPlanetario>()
                .HasOne(rp => rp.Planeta)
                .WithMany()
                .HasForeignKey(rp => rp.Id_Planeta)
                .OnDelete(DeleteBehavior.Restrict);


            // =========================
            // RESTRICCIONES Y CONVERSIONES
            // =========================

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Correo)
                .HasConversion(
                    v => v.Trim().ToLower(),
                    v => v
                );

            modelBuilder.Entity<Planeta>()
                .Property(p => p.Nivel_Tecnologico)
                .HasConversion<byte>();
        }
    }
}