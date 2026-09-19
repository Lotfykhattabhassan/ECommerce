using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Identity.Application.Abstractions.Persistence;
using MiniECommerce.Modules.Identity.Domain.Entities;
using MiniECommerce.Modules.Identity.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _dbContext;

        public UserRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(
            User user,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(user);

            await _dbContext.Users.AddAsync(user, cancellationToken);
        }

        public async Task<User?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            return await _dbContext.Users
                .Include(x=>x.Credential)
                .Include(x=>x.Roles)
                .ThenInclude(x=>x.Role)
                .FirstOrDefaultAsync(
                    u => u.Id == id,
                    cancellationToken);
        }

        public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .Users
                .Include(x => x.Credential)
                .Include(x => x.Roles)
                .ThenInclude(x => x.Role)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException(nameof(email));

            return await _dbContext.Users
                .AnyAsync(
                    u => u.Email == email,
                    cancellationToken);
        }
        public async Task<User?> GetUserByEmailAsync(string email,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException(nameof(email));

            return await _dbContext.Users
                .Include(x=>x.Credential)
                .Include(x=>x.Roles)
                .ThenInclude(x=>x.Role)
                .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }
        public void Delete(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            _dbContext.Users.Remove(user);
        }
    }
}
