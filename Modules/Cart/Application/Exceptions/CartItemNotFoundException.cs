
namespace MiniECommerce.Modules.Cart.Application.Exceptions
{
    public class CartItemNotFoundException : Exception
    {
        public CartItemNotFoundException()
            : base("CartItem Not Found")
        {
            
        }
    }
}
