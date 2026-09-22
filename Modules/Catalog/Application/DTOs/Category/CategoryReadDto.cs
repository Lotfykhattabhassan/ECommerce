
namespace MiniECommerce.Modules.Catalog.Application.DTOs.Category
{
    public class CategoryReadDto
    {
        public string Name { get; private set; } = null!;

        public string? Description { get; private set; }

        public Guid? ParentCategoryId { get; private set; }
    }
}
