using MiniECommerce.Modules.Cart.Domain.Entities;
using MiniECommerce.Modules.Cart.Domain.Enums;

namespace MiniECommerce.Modules.Cart.Application.DTOs.Cart
{
    public class CartReadDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public CartStatus Status { get; set; }
        public IReadOnlyList<Domain.Entities.CartItem> CartItems { get; set; }
    }
}
