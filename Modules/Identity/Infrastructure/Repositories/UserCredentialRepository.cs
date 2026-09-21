
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Identity.Application.Abstractions.Persistence;
using MiniECommerce.Modules.Identity.Domain.Entities;
using MiniECommerce.Modules.Identity.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Identity.Infrastructure.Repositories
{
    public class UserCredentialRepository : IUserCredentialRepository
    {
        private readonly IdentityDbContext _dbContext;

        public UserCredentialRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(UserCredential credential, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(credential);

            await _dbContext.UserCredentials.AddAsync(credential, cancellationToken);
        }

        public async Task<UserCredential?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            return await _dbContext
                .UserCredentials
                .FirstOrDefaultAsync(c => c.UserId == userId,cancellationToken);

        }

    }
}
