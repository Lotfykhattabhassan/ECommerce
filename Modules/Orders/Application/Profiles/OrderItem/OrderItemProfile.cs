using AutoMapper;
using MiniECommerce.Modules.Orders.Application.DTOs.OrderItem;

namespace MiniECommerce.Modules.Orders.Application.Profiles.OrderItem
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<Domain.Entities.OrderItem, OrderItemReadDto>();
        }
    }
}
