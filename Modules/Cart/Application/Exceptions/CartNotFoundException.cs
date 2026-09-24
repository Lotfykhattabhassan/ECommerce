
namespace MiniECommerce.Modules.Cart.Application.Exceptions
{
    public class CartNotFoundException : Exception
    {
        public CartNotFoundException()
            : base("Cart Not Found")
        {
            
        }
    }
}
