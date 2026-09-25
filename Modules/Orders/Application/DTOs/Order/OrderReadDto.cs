using MiniECommerce.Modules.Orders.Domain.Enums;

namespace MiniECommerce.Modules.Orders.Application.DTOs.Order
{
    public class OrderReadDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus Status { get; set; }
        public IReadOnlyList<Domain.Entities.OrderItem> OrderItems { get; set; }
    }
}
