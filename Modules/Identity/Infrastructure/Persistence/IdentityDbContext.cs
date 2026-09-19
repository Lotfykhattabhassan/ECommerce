using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Identity.Domain.Entities;
using MiniECommerce.Modules.Identity.Infrastructure.Persistence.Configurations;

namespace MiniECommerce.Modules.Identity.Infrastructure.Persistence;

public class IdentityDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserCredential> UserCredentials => Set<UserCredential>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(UserConfiguration).Assembly);
    }
}