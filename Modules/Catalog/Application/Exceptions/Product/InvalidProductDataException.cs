
namespace MiniECommerce.Modules.Catalog.Application.Exceptions.Product
{
    public class InvalidProductDataException : Exception
    {
        public InvalidProductDataException() 
            : base ("This product data is not valid")
        {
            
        }
    }
}
