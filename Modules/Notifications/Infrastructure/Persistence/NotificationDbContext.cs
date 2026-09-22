using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Notifications.Domain.Entities;
using MiniECommerce.Modules.Notifications.Infrastructure.Persistence.Configurations;

namespace MiniECommerce.Modules.Notifications.Infrastructure.Persistence
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options)
      : base(options)
        {
        }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationConfiguration).Assembly);
        }

    }
}
