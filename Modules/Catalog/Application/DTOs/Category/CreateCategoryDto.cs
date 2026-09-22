
namespace MiniECommerce.Modules.Catalog.Application.DTOs.Category
{
    public class CreateCategoryDto
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public Guid? ParentCategoryId { get; set; }
    }
}
