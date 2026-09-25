using AutoMapper;
using MiniECommerce.Modules.Orders.Application.DTOs.Order;

namespace MiniECommerce.Modules.Orders.Application.Profiles.Order
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Domain.Entities.Order, OrderReadDto>();
        }
    }
}
