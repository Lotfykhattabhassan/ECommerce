using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Inventory.Domain.Entities;

namespace MiniECommerce.Modules.Inventory.Infrastructure.Persistence
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductInventory> ProductInventories => Set<ProductInventory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
