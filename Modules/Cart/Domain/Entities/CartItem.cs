using MiniECommerce.BuildingBlocks.Domain.Common;

namespace MiniECommerce.Modules.Cart.Domain.Entities
{
    public class CartItem : Entity<Guid>
    {
        public Guid CartId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public Cart Cart { get; set; }
        private CartItem() { }
        private CartItem(Guid cartId,
            Guid productId,
            int quantity,
            decimal unitPrice)
        {
            if (cartId == Guid.Empty)
                throw new ArgumentException(nameof(cartId));
            CartId = cartId;

            if (productId == Guid.Empty)
                throw new ArgumentException(nameof(productId));
            ProductId = productId;

            if(quantity <= 0)
                throw new ArgumentException(nameof(quantity));
            Quantity = quantity;

            if (unitPrice <= 0)
                throw new ArgumentException(nameof(unitPrice));
            UnitPrice = unitPrice;
        }
        public static CartItem Create(Guid cartId,
            Guid productId,
            int quantity,
            decimal unitPrice)
        {
            return new CartItem(cartId, productId, quantity, unitPrice);
        }
        public void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException(nameof(quantity));

            Quantity += quantity;
            MarkAsUpdated();
        }
        public void DecreaseQuantity(int quantity)
        {
            if (quantity <= 0 || quantity > Quantity )
                throw new ArgumentException(nameof(quantity));

            Quantity -= quantity;
            MarkAsUpdated();
        }
        public void ChangeQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException(nameof(quantity));

            Quantity = quantity;
            MarkAsUpdated();
        }
    }
}
