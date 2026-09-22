namespace MiniECommerce.Modules.Catalog.Application.DTOs.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get;set; }

        public string SKU { get; set; } = null!;

        public Guid CategoryId { get; set; }
    }
}
