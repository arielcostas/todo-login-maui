using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .OwnsMany(u => u.Tareas)
                .WithOwner(t => t.Creador);
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
    }
}
