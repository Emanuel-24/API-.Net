using Microsoft.EntityFrameworkCore;
using WebApiFinal.Models;

namespace WebApiFinal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Servicio> Servicios { get; set; } = null!;
        public DbSet<Reserva> Reservas { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;

        public DbSet<ServicioReserva> ServiciosReservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Servicio>()
                .Property(s => s.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ServicioReserva>()
                .HasKey(sr => new { sr.ServicioId, sr.ReservaId });

            modelBuilder.Entity<ServicioReserva>()
                .HasOne(sr => sr.Servicio)
                .WithMany(s => s.ServiciosReservas)
                .HasForeignKey(sr => sr.ServicioId)
                .OnDelete(DeleteBehavior.Restrict);   // 🔑 IMPORTANTE

            modelBuilder.Entity<ServicioReserva>()
                .HasOne(sr => sr.Reserva)
                .WithMany(r => r.ServiciosReservas)
                .HasForeignKey(sr => sr.ReservaId)
                .OnDelete(DeleteBehavior.Restrict);   // 🔑 IMPORTANTE
        }
    }
}
