using MiniECommerce.Modules.Orders.Application.Abstractions;

namespace MiniECommerce.Modules.Orders.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderDbContext _context;

        public UnitOfWork(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(
                cancellationToken);
        }
    }
}