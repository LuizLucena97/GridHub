using GridHub.Database.Mappings;
using GridHub.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GridHub.Database
{
    public class FIAPDBContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public FIAPDBContext(DbContextOptions<FIAPDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMapping());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // Geração de ID para novos usuários
            foreach (var entry in ChangeTracker.Entries<Usuario>()
                .Where(e => e.State == EntityState.Added))
            {
                // Se não foi definido um ID, encontrar o próximo disponível
                if (entry.Entity.UsuarioId == 0)
                {
                    var maxId = Usuarios.Any() ? Usuarios.Max(u => u.UsuarioId) : 0;
                    entry.Entity.UsuarioId = maxId + 1;
                }
            }

            return base.SaveChanges();
        }
    }
}
