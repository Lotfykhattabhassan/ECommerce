using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Cart.Domain.Entities;
using MiniECommerce.Modules.Cart.Infrastructure.Persistence.Configurations;

namespace MiniECommerce.Modules.Cart.Infrastructure.Persistence
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Domain.Entities.Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CartConfiguration).Assembly);
        }
    }
}
