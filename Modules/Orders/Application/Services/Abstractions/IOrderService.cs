using MiniECommerce.Modules.Orders.Application.DTOs.Order;

namespace MiniECommerce.Modules.Orders.Application.Services.Abstractions
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(
            CancellationToken cancellationToken = default);

        Task<OrderReadDto> GetByIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task<OrderReadDto> GetMyOrderByIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<OrderReadDto>> GetMyOrdersAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<OrderReadDto>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task ConfirmAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task CancelAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task CompleteAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);
    }
}