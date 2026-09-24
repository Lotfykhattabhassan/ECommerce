using MiniECommerce.Modules.Cart.Application.DTOs.CartItem;

namespace MiniECommerce.Modules.Cart.Application.Services.Abstractions
{
    public interface ICartItemService
    {
        Task<CartItemReadDto> GetByIdAsync(
            Guid cartItemId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<CartItemReadDto>> GetByCartIdAsync(
            Guid cartId,
            CancellationToken cancellationToken = default);

        Task IncreaseQuantityAsync(
            Guid cartItemId,
            int quantity,
            CancellationToken cancellationToken = default);

        Task DecreaseQuantityAsync(
            Guid cartItemId,
            int quantity,
            CancellationToken cancellationToken = default);

        Task ChangeQuantityAsync(
            Guid cartItemId,
            int quantity,
            CancellationToken cancellationToken = default);

        Task RemoveAsync(
            Guid cartItemId,
            CancellationToken cancellationToken = default);
    }
}