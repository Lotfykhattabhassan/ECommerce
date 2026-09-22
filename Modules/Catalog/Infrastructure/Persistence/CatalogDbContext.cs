using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Catalog.Domain.Entities;
using MiniECommerce.Modules.Catalog.Infrastructure.Persistence.Configurations;

namespace MiniECommerce.Modules.Catalog.Infrastructure.Persistence
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            :base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }
    }
}
