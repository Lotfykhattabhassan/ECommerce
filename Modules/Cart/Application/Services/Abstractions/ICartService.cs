using MiniECommerce.Modules.Cart.Application.DTOs.Cart;

namespace MiniECommerce.Modules.Cart.Application.Services.Abstractions
{
    public interface ICartService
    {
        Task<Guid> CreateCartAsync(CancellationToken cancellationToken = default);

        Task<CartReadDto> GetCartByIdAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<CartReadDto> GetMyActiveCartAsync(
            CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<CartReadDto>> GetMyCartsAsync(
            CancellationToken cancellationToken = default);
        Task AddItemAsync(
            AddCartItemDto dto,
            CancellationToken cancellationToken = default);
        Task<CartReadDto> GetCartByIdAndUserId(
            Guid cartId,
            CancellationToken cancellationToken = default);

        Task ChangeItemQuantityAsync(
            ChangeCartItemQuantityDto dto,
            CancellationToken cancellationToken = default);

        Task RemoveItemAsync(
            Guid productId,
            CancellationToken cancellationToken = default);

        Task ClearCartAsync(
            CancellationToken cancellationToken = default);

        Task CheckoutAsync(
            CancellationToken cancellationToken = default);

        Task AbandonCartAsync(
            CancellationToken cancellationToken = default);
    }
}