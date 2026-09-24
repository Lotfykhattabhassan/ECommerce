using MiniECommerce.Modules.Cart.Domain.Entities;

namespace MiniECommerce.Modules.Cart.Application.Abstractions
{
    public interface ICartItemRepository
    {
        Task AddAsync(
            CartItem cartItem,
            CancellationToken cancellationToken = default);

        Task<CartItem?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<CartItem?> GetByCartIdAndProductIdAsync(
            Guid cartId,
            Guid productId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CartItem>> GetByCartIdAsync(
            Guid cartId,
            CancellationToken cancellationToken = default);
    }
}