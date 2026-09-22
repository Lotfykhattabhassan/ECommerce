
namespace MiniECommerce.Modules.Catalog.Application.Exceptions.Category
{
    public class InvalidCategoryDataException : Exception
    {
        public InvalidCategoryDataException()
            : base("This Category data is not valid")
        {

        }
    }
}
