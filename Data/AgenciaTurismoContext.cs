using AgenciaTurismo.Models;
using Microsoft.EntityFrameworkCore;

namespace AgenciaTurismo.Data
{
    public class AgenciaTurismoContext : DbContext
    {
        public AgenciaTurismoContext(DbContextOptions<AgenciaTurismoContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PaisDestino> PaisesDestino { get; set; }
        public DbSet<CidadeDestino> CidadesDestino { get; set; }
        public DbSet<PacoteTuristico> PacotesTuristicos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Relacionamento 1:N (Um-para-Muitos) entre Cliente e Reserva ---
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Reservas) // Um Cliente TEM MUITAS Reservas
                .WithOne(r => r.Cliente)  // Cada Reserva TEM UM Cliente
                .HasForeignKey(r => r.ClienteId) // A chave estrangeira é ClienteId em Reserva
                .OnDelete(DeleteBehavior.Cascade); // Se um cliente for deletado, suas reservas também são.

            // --- Relacionamento 1:N (Um-para-Muitos) entre PacoteTuristico e Reserva ---
            modelBuilder.Entity<PacoteTuristico>()
                .HasMany(p => p.Reservas) // Um Pacote TEM MUITAS Reservas
                .WithOne(r => r.PacoteTuristico) // Cada Reserva TEM UM Pacote
                .HasForeignKey(r => r.PacoteTuristicoId)
                .OnDelete(DeleteBehavior.Restrict); // Impede deletar um pacote se ele tiver reservas.

            // --- Relacionamento N:N (Muitos-para-Muitos) entre PacoteTuristico e CidadeDestino ---
            modelBuilder.Entity<PacoteTuristico>()
                .HasMany(p => p.Destinos) // Um Pacote TEM MUITOS Destinos
                .WithMany(); // E uma Cidade pode estar em MUITOS Pacotes.
                             
        }
    }
}