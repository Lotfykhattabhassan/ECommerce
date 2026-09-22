
using MiniECommerce.Modules.Catalog.Domain.Entities;

namespace MiniECommerce.Modules.Catalog.Application.Abstractions
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<Product>> GetProductsByCategory(Guid categoryId, CancellationToken cancellationToken = default);
        Task<Product?> GetProductBySkuAsync(
            string sku,
            CancellationToken cancellationToken);

        Task<Product?> GetDeletedProductByIdAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}
