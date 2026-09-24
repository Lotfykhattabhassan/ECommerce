using MiniECommerce.Modules.Inventory.Domain.Entities;

namespace MiniECommerce.Modules.Inventory.Application.Abstractions
{
    public interface IProductInventoryRepository
    {
        Task AddAsync(ProductInventory inventory, CancellationToken cancellationToken = default);
        Task<ProductInventory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ProductInventory?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ProductInventory>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ProductInventory?> GetDeletedByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}
