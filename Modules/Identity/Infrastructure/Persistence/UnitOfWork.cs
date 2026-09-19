
using MiniECommerce.Modules.Identity.Application.Abstractions.Persistence;

namespace MiniECommerce.Modules.Identity.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityDbContext _dbContext;

        public UnitOfWork(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
