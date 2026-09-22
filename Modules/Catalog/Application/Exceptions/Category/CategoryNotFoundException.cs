
namespace MiniECommerce.Modules.Catalog.Application.Exceptions.Category
{
    internal class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException()
            : base("Category Not Found")
        {

        }
    }
}
