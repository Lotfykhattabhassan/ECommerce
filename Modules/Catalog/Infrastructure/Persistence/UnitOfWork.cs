using MiniECommerce.Modules.Catalog.Application.Abstractions;

namespace MiniECommerce.Modules.Catalog.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogDbContext _context;
        public UnitOfWork(CatalogDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
