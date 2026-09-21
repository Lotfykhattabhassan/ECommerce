using MiniECommerce.Modules.Identity.Domain.Entities;

namespace MiniECommerce.Modules.Identity.Application.Abstractions.Persistence
{
    public interface IUserCredentialRepository
    {
        Task AddAsync(UserCredential credential, CancellationToken cancellationToken = default);
        Task<UserCredential?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    }
}
