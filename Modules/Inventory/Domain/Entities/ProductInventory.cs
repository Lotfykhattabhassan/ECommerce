using MiniECommerce.BuildingBlocks.Domain.Common;

namespace MiniECommerce.Modules.Inventory.Domain.Entities
{
    public class ProductInventory : Entity<Guid>
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public int ReservedQuantity { get; private set; }

        private ProductInventory() { }

        private ProductInventory(Guid id, Guid productId, int quantity)
            : base(id)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException(nameof(productId));

            if (quantity < 0)
                throw new ArgumentException(nameof(quantity));

            ProductId = productId;
            Quantity = quantity;
            ReservedQuantity = 0;
        }

        public static ProductInventory Create(Guid productId, int quantity = 0)
        {
            var id = Guid.NewGuid();
            return new ProductInventory(id,productId, quantity);
        }

        public int GetAvailableQuantity()
        {
            return Quantity - ReservedQuantity;
        }

        public bool HasAvailableQuantity(int quantity)
        {
            if (quantity <= 0)
                return false;

            return GetAvailableQuantity() >= quantity;
        }

        public void AddStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException(nameof(quantity));

            Quantity += quantity;
            MarkAsUpdated();
        }

        public void RemoveStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException(nameof(quantity));

            if (!HasAvailableQuantity(quantity))
                throw new InvalidOperationException("Not enough available stock.");

            Quantity -= quantity;
            MarkAsUpdated();
        }

        public void ReserveStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException(nameof(quantity));

            if (!HasAvailableQuantity(quantity))
                throw new InvalidOperationException("Not enough available stock.");

            ReservedQuantity += quantity;
            MarkAsUpdated();
        }

        public void ReleaseReservedStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException(nameof(quantity));

            if (quantity > ReservedQuantity)
                throw new InvalidOperationException("Reserved quantity is not enough.");

            ReservedQuantity -= quantity;
            MarkAsUpdated();
        }
    }
}
