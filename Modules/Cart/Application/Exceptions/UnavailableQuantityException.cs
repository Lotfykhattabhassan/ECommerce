
namespace MiniECommerce.Modules.Cart.Application.Exceptions
{
    public class UnavailableQuantityException : Exception
    {
        public UnavailableQuantityException()
            : base("Unavailable Quantity")
        {
            
        }
    }
}
