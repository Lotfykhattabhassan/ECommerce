using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Inventory.Application.Abstractions;
using MiniECommerce.Modules.Inventory.Domain.Entities;
using MiniECommerce.Modules.Inventory.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Inventory.Infrastructure.Repositories
{
    public class ProductInventoryRepository : IProductInventoryRepository
    {
        private readonly InventoryDbContext _context;

        public ProductInventoryRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProductInventory inventory, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(inventory);
            await _context.ProductInventories.AddAsync(inventory, cancellationToken);
        }

        public Task<ProductInventory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            return _context.ProductInventories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<ProductInventory?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException(nameof(productId));

            return _context.ProductInventories.FirstOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
        }

        public async Task<IReadOnlyList<ProductInventory>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ProductInventories
                .AsNoTracking()
                .OrderBy(x => x.ProductId)
                .ToListAsync(cancellationToken);
        }

        public Task<ProductInventory?> GetDeletedByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException(nameof(productId));

            return _context.ProductInventories
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.ProductId == productId && x.IsDeleted, cancellationToken);
        }
    }
}
