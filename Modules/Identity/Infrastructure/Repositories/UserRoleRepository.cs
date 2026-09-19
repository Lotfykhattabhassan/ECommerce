using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Identity.Application.Abstractions.Persistence;
using MiniECommerce.Modules.Identity.Domain.Entities;
using MiniECommerce.Modules.Identity.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Identity.Infrastructure.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly IdentityDbContext _dbContext;

    public UserRoleRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        UserRole userRole,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(userRole);

        await _dbContext.UserRoles.AddAsync(userRole, cancellationToken);
    }

    public async Task<UserRole?> GetAsync(
        Guid userId,
        int roleId,
        CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException(nameof(userId));

        if (roleId <= 0)
            throw new ArgumentException(nameof(roleId));

        return await _dbContext.UserRoles
            .FirstOrDefaultAsync(
                x => x.UserId == userId && x.RoleId == roleId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<UserRole>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException(nameof(userId));

        return await _dbContext.UserRoles
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public void Delete(UserRole userRole)
    {
        ArgumentNullException.ThrowIfNull(userRole);

        _dbContext.UserRoles.Remove(userRole);
    }
}
