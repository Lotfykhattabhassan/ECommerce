
namespace MiniECommerce.Modules.Cart.Application.Exceptions
{
    public class InvalidCartItemDataException : Exception
    {
        public InvalidCartItemDataException()
            : base("this cartItem data is not valid")
        {
            
        }
    }
}
