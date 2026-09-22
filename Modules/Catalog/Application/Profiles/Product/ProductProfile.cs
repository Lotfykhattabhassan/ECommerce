using AutoMapper;
using MiniECommerce.Modules.Catalog.Application.DTOs.Product;
using MiniECommerce.Modules.Catalog.Domain.Entities;

namespace MiniECommerce.Modules.Catalog.Application.Profiles.ProductProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductReadDto>();
        }
    }
}
