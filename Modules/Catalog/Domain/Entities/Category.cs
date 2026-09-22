using MiniECommerce.BuildingBlocks.Domain.Common;

namespace MiniECommerce.Modules.Catalog.Domain.Entities
{
    public class Category : Entity<Guid>
    {
        public string Name { get; private set; }

        public string? Description { get; private set; }

        public Guid? ParentCategoryId { get; private set; }

        private Category()
        {
            
        }
        private Category(string name,
            string? description,
            Guid? parentCategoryId)
        {
            ArgumentNullException.ThrowIfNull(name);
            Name = name;

            Description = description;

            ParentCategoryId = parentCategoryId;
        }

        public static Category Create(string name,
            string? description,
            Guid? parentCategoryId)
        {
            return new Category(name, description, parentCategoryId);
        }

        public void ChangeCategoryName(string name)
        {
            ArgumentNullException.ThrowIfNull(name);
            Name = name;

            MarkAsUpdated();
        }
        public void ChangeCategoryDescription(string? description)
        {
            Description = description;

            MarkAsUpdated();
        }
        public void ChangeCategoryParentCategory(Guid? parentCategoryId)
        {

            ParentCategoryId = parentCategoryId;
            MarkAsUpdated();
        }
    }
}
