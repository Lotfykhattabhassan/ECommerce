using MiniECommerce.Modules.Identity.Domain.Entities;

namespace MiniECommerce.Modules.Identity.Application.Abstractions.Persistence;

public interface IRoleRepository
{
    Task AddAsync(
        Role role,
        CancellationToken cancellationToken = default);

    Task<Role?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<Role?> GetByNameAsync(
        string roleName,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByNameAsync(
        string roleName,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Role>> GetAllAsync(
        CancellationToken cancellationToken = default);
}
