using MiniECommerce.Modules.Orders.Domain.Entities;

namespace MiniECommerce.Modules.Orders.Application.Abstractions
{
    public interface IOrderRepository
    {
        Task AddAsync(
            Order order,
            CancellationToken cancellationToken = default);

        Task<Order?> GetByIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task<Order?> GetOrderByIdAndUserIdAsync(
            Guid orderId,
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Order>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Order>> GetAllAsync(
            CancellationToken cancellationToken = default);
    }
}