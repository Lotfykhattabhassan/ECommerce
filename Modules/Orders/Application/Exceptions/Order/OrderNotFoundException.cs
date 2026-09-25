
namespace MiniECommerce.Modules.Orders.Application.Exceptions.Order
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException()
            : base("Order Not Found")
        {
            
        }
    }
}
