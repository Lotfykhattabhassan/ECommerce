using Contract.Order.Dtos;

namespace Contract.Order.Abstractions
{
    public interface ICartOrder
    {
        Task<CartReadForOrderDto> GetCartInfoForOrder(Guid userId,
            CancellationToken cancellationToken = default);
        Task CheckedOutCartForOrder(Guid userId,
            CancellationToken cancellationToken = default);
    }
}
