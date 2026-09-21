using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Reviews.Domain.Entities;
using MiniECommerce.Modules.Reviews.Infrastructure.Persistence.Configurations;

namespace MiniECommerce.Modules.Reviews.Infrastructure.Persistence
{
    public class ReviewDbContext : DbContext
    {
        public ReviewDbContext(DbContextOptions<ReviewDbContext> options)
       : base(options)
        {
        }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReviewConfiguration).Assembly);
        }
    }
}
