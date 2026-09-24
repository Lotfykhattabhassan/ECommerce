using AutoMapper;
using MiniECommerce.Modules.Cart.Application.DTOs.Cart;

namespace MiniECommerce.Modules.Cart.Application.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Domain.Entities.Cart, CartReadDto>();
        }
    }
}
