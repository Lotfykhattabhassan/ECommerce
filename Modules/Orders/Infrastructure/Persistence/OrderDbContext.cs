using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Orders.Domain.Entities;
using MiniECommerce.Modules.Orders.Infrastructure.Persistence.Configurations;

namespace MiniECommerce.Modules.Orders.Infrastructure.Persistence
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            :base(options)
        {
            
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
        }
    }
}
