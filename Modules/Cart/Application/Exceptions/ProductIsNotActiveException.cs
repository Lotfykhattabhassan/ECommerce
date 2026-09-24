
namespace MiniECommerce.Modules.Cart.Application.Exceptions
{
    public class ProductIsNotActiveException : Exception
    {
        public ProductIsNotActiveException() 
            : base("Product Is Not Active")
        {
            
        }
    }
}
