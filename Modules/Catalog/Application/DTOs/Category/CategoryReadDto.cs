
namespace MiniECommerce.Modules.Catalog.Application.DTOs.Category
{
    public class CategoryReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public Guid? ParentCategoryId { get;  set; }
    }
}
