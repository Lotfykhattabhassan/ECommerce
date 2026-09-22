using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Catalog.Application.Abstractions;
using MiniECommerce.Modules.Catalog.Domain.Entities;
using MiniECommerce.Modules.Catalog.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _context;
        public ProductRepository(CatalogDbContext context)
        {
            _context = context;
        }
        public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(product);
            await _context.Products.AddAsync(product, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Product>> GetProductsByCategory(Guid categoryId, CancellationToken cancellationToken)
        {
            if (categoryId == Guid.Empty)
                throw new ArgumentException(nameof(categoryId));

            return await _context.Products
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync(cancellationToken);
        }
        public async Task<Product?> GetProductBySkuAsync(
            string sku,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException(nameof(sku));

            return await _context.Products
                .FirstOrDefaultAsync(x => x.SKU == sku, cancellationToken);
        }

        public async Task<Product?> GetDeletedProductByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            return await _context.Products
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
