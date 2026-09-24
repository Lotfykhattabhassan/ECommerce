using MiniECommerce.Modules.Cart.Application.Abstractions;
using MiniECommerce.Modules.Cart.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Cart.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CartDbContext _context;

        public UnitOfWork(CartDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}