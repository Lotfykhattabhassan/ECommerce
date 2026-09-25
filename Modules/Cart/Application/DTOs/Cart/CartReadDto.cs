using MiniECommerce.Modules.Cart.Application.DTOs.CartItem;
using MiniECommerce.Modules.Cart.Domain.Entities;
using MiniECommerce.Modules.Cart.Domain.Enums;

namespace MiniECommerce.Modules.Cart.Application.DTOs.Cart
{
    public class CartReadDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public CartStatus Status { get; set; }
        public IReadOnlyList<CartItemReadDto> CartItems { get; set; }
    }
}
