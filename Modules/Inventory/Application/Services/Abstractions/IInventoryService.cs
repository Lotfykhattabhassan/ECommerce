using MiniECommerce.Modules.Inventory.Application.DTOs;

namespace MiniECommerce.Modules.Inventory.Application.Services.Abstractions
{
    public interface IInventoryService
    {
        Task<Guid> CreateInventoryAsync(CreateInventoryDto dto, CancellationToken cancellationToken = default);
        Task<InventoryReadDto> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<InventoryReadDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddStockAsync(UpdateStockDto dto, CancellationToken cancellationToken = default);
        Task RemoveStockAsync(UpdateStockDto dto, CancellationToken cancellationToken = default);
        Task ReserveStockAsync(ReserveStockDto dto, CancellationToken cancellationToken = default);
        Task ReleaseReservedStockAsync(ReserveStockDto dto, CancellationToken cancellationToken = default);
        Task DeleteInventoryAsync(Guid productId, CancellationToken cancellationToken = default);
        Task RestoreInventoryAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}
