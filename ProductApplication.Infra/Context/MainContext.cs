using Microsoft.EntityFrameworkCore;
using ProductApplication.Domain.Entities;
using System.Linq;

namespace ProductApplication.Infra.Context
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions options)
            : base(options)
        {
        }

        public MainContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList().ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
                entityType.GetProperties().Where(c => c.ClrType == typeof(string)).ToList()
                    .ForEach(p => p.SetIsUnicode(false));
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer("DefaultConnection");
            }
        }
        public DbSet<Supplier> suppliers { get; set; }
        public DbSet<Category> categories { get; set; }

    }
}
