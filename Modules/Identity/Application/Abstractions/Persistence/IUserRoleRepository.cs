using MiniECommerce.Modules.Identity.Domain.Entities;

namespace MiniECommerce.Modules.Identity.Application.Abstractions.Persistence;

public interface IUserRoleRepository
{
    Task AddAsync(
        UserRole userRole,
        CancellationToken cancellationToken = default);

    Task<UserRole?> GetAsync(
        Guid userId,
        int roleId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<UserRole>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    void Delete(UserRole userRole);
}
