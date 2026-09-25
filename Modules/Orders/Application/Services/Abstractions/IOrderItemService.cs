using MiniECommerce.Modules.Orders.Application.DTOs.OrderItem;

namespace MiniECommerce.Modules.Orders.Application.Services.Abstractions
{
    public interface IOrderItemService
    {
        Task<OrderItemReadDto> GetByIdAsync(
            Guid orderItemId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<OrderItemReadDto>> GetByOrderIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task ChangeQuantityAsync(
            Guid orderItemId,
            int quantity,
            CancellationToken cancellationToken = default);
    }
}