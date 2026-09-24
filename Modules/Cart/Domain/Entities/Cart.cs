using MiniECommerce.BuildingBlocks.Domain.Common;
using MiniECommerce.Modules.Cart.Domain.Enums;

namespace MiniECommerce.Modules.Cart.Domain.Entities
{
    public class Cart : Entity<Guid>
    {
        public Guid UserId { get; private set; }
        public CartStatus Status { get; private set; }

        private readonly List<CartItem> _cartItems = [];
        public IReadOnlyCollection<CartItem> CartItems
            => _cartItems.AsReadOnly();

        private Cart() { }
        private Cart(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            UserId = userId;
            Status = CartStatus.Active;
        }
        public static Cart Create(Guid userId)
        {
            return new Cart(userId);
        }
        public void AddItem(
            Guid productId,
            decimal unitPrice,
            int quantity)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException(nameof(productId));

            if (quantity <= 0)
                throw new ArgumentException(nameof(quantity));

            if (unitPrice <= 0)
                throw new ArgumentException(nameof(unitPrice));

            var existingItem = _cartItems
                .FirstOrDefault(x => x.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(quantity);
            }
            else
            {
                _cartItems.Add(
                    CartItem.Create(
                        Id,
                        productId,
                        quantity,
                        unitPrice));
            }

            MarkAsUpdated();
        }
        public void ChangeItemQuantity(Guid productId,int quantity)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException(nameof(productId));
            if (quantity <= 0)
                throw new ArgumentException(nameof(quantity));

            var cartItem = _cartItems.FirstOrDefault(x => x.ProductId == productId);
            if (cartItem == null)
                throw new InvalidOperationException("Cart item not found.");

            cartItem.ChangeQuantity(quantity);

            MarkAsUpdated();
        }
        public void RemoveItem(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException(nameof(productId));

            var cartItem = _cartItems.FirstOrDefault(x => x.ProductId == productId);
            if (cartItem == null)
                throw new InvalidOperationException("Cart item not found.");

            _cartItems.Remove(cartItem);
            MarkAsUpdated();
        }
        public void Clear()
        {
            _cartItems.Clear();
            MarkAsUpdated();
        }
        public void MarkAsCheckedOut()
        {
            if (Status == CartStatus.CheckedOut)
                return;

            Status = CartStatus.CheckedOut;
            MarkAsUpdated();
        }
        public void MarkAsAbandoned()
        {
            if (Status == CartStatus.Abandoned)
                return;

            Status = CartStatus.Abandoned;
            MarkAsUpdated();
        }
    }
}
