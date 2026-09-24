using AutoMapper;
using MiniECommerce.Modules.Cart.Application.DTOs.CartItem;
using MiniECommerce.Modules.Cart.Domain.Entities;

namespace MiniECommerce.Modules.Cart.Application.Profiles
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItem, CartItemReadDto>();
        }
    }
}
