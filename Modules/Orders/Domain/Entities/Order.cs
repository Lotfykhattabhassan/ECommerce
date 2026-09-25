using MiniECommerce.BuildingBlocks.Domain.Common;
using MiniECommerce.Modules.Orders.Domain.Enums;

namespace MiniECommerce.Modules.Orders.Domain.Entities
{
    public class Order : Entity<Guid>
    {
        public Guid UserId { get; private set; }
        public OrderStatus Status { get; private set; }

        private readonly List<OrderItem> _orderItems = [];
        public IReadOnlyList<OrderItem> OrderItems =>
            _orderItems.AsReadOnly();

        private Order() { }
        private Order(Guid id, Guid userId)
            : base(id)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            UserId = userId;

            Status = OrderStatus.PendingPayment;
        }
        public static Order Create(Guid userId)
        {
            var id = Guid.NewGuid();
            return new Order(id,userId);
        }
        public void AddItem(
            Guid productId,
            string productName,
            decimal unitPrice,
            int quantity)
        {
            if (Status != OrderStatus.PendingPayment)
                throw new InvalidOperationException(
                    "Items can only be modified while order is pending payment.");

            var orderItem = OrderItem.Create(Id,
                productId,
                productName,
                unitPrice,
                quantity);

            _orderItems.Add(orderItem);
            MarkAsUpdated();
        }
        public void Confirm()
        {
            if (Status != OrderStatus.PendingPayment)
                throw new InvalidOperationException(
                    "Only pending payment orders can be confirmed.");

            Status = OrderStatus.Confirmed;
            MarkAsUpdated();
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Completed)
                throw new InvalidOperationException(
                    "Completed orders cannot be cancelled.");

            if (Status == OrderStatus.Cancelled)
                return;

            Status = OrderStatus.Cancelled;
            MarkAsUpdated();
        }

        public void Complete()
        {
            if (Status != OrderStatus.Confirmed)
                throw new InvalidOperationException(
                    "Only confirmed orders can be completed.");

            Status = OrderStatus.Completed;
            MarkAsUpdated();
        }
        
    }
}
