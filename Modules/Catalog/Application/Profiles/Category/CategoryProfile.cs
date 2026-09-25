
using AutoMapper;
using MiniECommerce.Modules.Catalog.Application.DTOs.Category;
using MiniECommerce.Modules.Catalog.Domain.Entities;

namespace MiniECommerce.Modules.Catalog.Application.Profiles.Category
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Domain.Entities.Category, CategoryReadDto>();
        }
    }
}
