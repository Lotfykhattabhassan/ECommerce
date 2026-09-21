using MiniECommerce.Modules.Reviews.Application.Abstractions;

namespace MiniECommerce.Modules.Reviews.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReviewDbContext _context;
        public UnitOfWork(ReviewDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
