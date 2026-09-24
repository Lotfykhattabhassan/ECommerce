
namespace MiniECommerce.Modules.Cart.Application.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException()
            : base("Product Not Found")
        {
            
        }
    }
}
