using MiniECommerce.Modules.Orders.Domain.Entities;

namespace MiniECommerce.Modules.Orders.Application.Abstractions
{
    public interface IOrderItemRepository
    {
        Task<OrderItem?> GetByIdAsync(
            Guid orderItemId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<OrderItem>> GetByOrderIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            OrderItem orderItem,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            OrderItem orderItem,
            CancellationToken cancellationToken = default);
    }
}