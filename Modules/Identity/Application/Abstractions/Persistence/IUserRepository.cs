using MiniECommerce.Modules.Identity.Domain.Entities;

namespace MiniECommerce.Modules.Identity.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task AddAsync(User user,CancellationToken cancellationToken = default);

    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email,
           CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);

    void Delete(User user);
}