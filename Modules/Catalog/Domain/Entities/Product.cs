using MiniECommerce.BuildingBlocks.Domain.Common;
using MiniECommerce.Modules.Catalog.Domain.Enums;

namespace MiniECommerce.Modules.Catalog.Domain.Entities
{
    public class Product : Entity<Guid>
    {
        public string Name { get; private set; }

        public string? Description { get; private set; }

        public decimal Price { get; private set; }

        public string SKU { get; private set; }

        public Guid CategoryId { get; private set; }

        public ProductStatus Status { get; private set; }

        private Product() { }

        private Product(string name,
           string? description,
           decimal price,
           string sku,
           Guid categoryId)
        {
            ArgumentNullException.ThrowIfNull(name);
            Name = name;

            Description = description;

            if (price <= 0) throw new ArgumentException(nameof(price));
            Price = price;

            ArgumentNullException.ThrowIfNull(sku);
            SKU = sku;

            if(categoryId == Guid.Empty) throw new ArgumentException(nameof(categoryId));
            CategoryId = categoryId;

            Status = ProductStatus.Draft;
        }

        public static Product Create(string name,
           string? description,
           decimal price,
           string sku,
           Guid categoryId)
        {
            return new Product(name, description, price, sku, categoryId);
        }

        public void ChangeProductName(string name)
        {
            ArgumentNullException.ThrowIfNull(name);

            Name = name;
            MarkAsUpdated();
        }
        public void ChangeProductDescription(string? description)
        {
            Description = description;
            MarkAsUpdated();
        }
        public void ChangeProductSKU(string sku)
        {
            ArgumentNullException.ThrowIfNull(sku);

            SKU = sku;
            MarkAsUpdated();
        }
        public void ChangeProductPrice(decimal price)
        {
            if (price <= 0) throw new ArgumentException(nameof(price));

            Price = price;
            MarkAsUpdated();
        }
        public void ChangeProductCategory(Guid categoryId)
        {
            if (categoryId == Guid.Empty) 
                throw new ArgumentException(nameof(categoryId));

            CategoryId = categoryId;
            MarkAsUpdated();
        }
        public void Activate()
        {
            if (Status == ProductStatus.Active)
                return;

            Status = ProductStatus.Active;
            MarkAsUpdated();
        }
        public void Deactivate()
        {
            if (Status == ProductStatus.Inactive)
                return;

            Status = ProductStatus.Inactive;
            MarkAsUpdated();
        }
    }
}
