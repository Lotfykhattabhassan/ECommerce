using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Identity.Application.Abstractions.Persistence;
using MiniECommerce.Modules.Identity.Domain.Entities;
using MiniECommerce.Modules.Identity.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Identity.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly IdentityDbContext _dbContext;

    public RoleRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Role role,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(role);

        await _dbContext.Roles.AddAsync(role, cancellationToken);
    }

    public async Task<Role?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            throw new ArgumentException(nameof(id));

        return await _dbContext.Roles
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Role?> GetByNameAsync(
        string roleName,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            throw new ArgumentException(nameof(roleName));

        return await _dbContext.Roles
            .FirstOrDefaultAsync(x => x.RoleName == roleName, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(
        string roleName,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            throw new ArgumentException(nameof(roleName));

        return await _dbContext.Roles
            .AnyAsync(x => x.RoleName == roleName, cancellationToken);
    }

    public async Task<IReadOnlyList<Role>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public void Delete(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);

        _dbContext.Roles.Remove(role);
    }
}
