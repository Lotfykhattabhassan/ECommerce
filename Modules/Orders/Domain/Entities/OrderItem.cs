using MiniECommerce.BuildingBlocks.Domain.Common;

namespace MiniECommerce.Modules.Orders.Domain.Entities
{
    public class OrderItem : Entity<Guid>
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; } = string.Empty;
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public Order Order { get; private set; }
        private OrderItem() { }
        private OrderItem(Guid id,
            Guid orderId,
            Guid productId,
            string productName,
            decimal unitPrice,
            int quantity)
            : base(id)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException(nameof(orderId));
            OrderId = orderId;

            if (productId == Guid.Empty)
                throw new ArgumentException(nameof(productId));
            ProductId = productId;

            if(string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException(nameof(productName));
            ProductName = productName;

            if(unitPrice <= 0)
                throw new ArgumentException(nameof(unitPrice));
            UnitPrice = unitPrice;

            if (quantity <= 0)
                throw new ArgumentException(nameof(quantity));
            Quantity = quantity;
        }
        public static OrderItem Create(Guid orderId,
            Guid productId,
            string productName,
            decimal unitPrice,
            int quantity)
        {
            return new OrderItem(Guid.NewGuid(),
                orderId,
                productId,
                productName,
                unitPrice,
                quantity);
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
