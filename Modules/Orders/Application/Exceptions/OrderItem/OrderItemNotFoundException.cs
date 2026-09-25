
namespace MiniECommerce.Modules.Orders.Application.Exceptions.OrderItem
{
    public class OrderItemNotFoundException : Exception
    {
        public OrderItemNotFoundException()
            : base("OrderItem Not Found")
        {
            
        }
    }
}
