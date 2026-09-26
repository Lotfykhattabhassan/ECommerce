using Microsoft.EntityFrameworkCore;

namespace MiniECommerce.Modules.Payment.Infrastructure.Persistence
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Entities.Payment> Payments => Set<Domain.Entities.Payment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(PaymentDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}