
namespace MiniECommerce.Modules.Cart.Application.DTOs.Cart
{
    public record ChangeCartItemQuantityDto(Guid ProductId,
            int Quantity);
}
