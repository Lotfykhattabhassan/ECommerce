using AutoMapper;
using MiniECommerce.Modules.Reviews.Application.DTOs;
using MiniECommerce.Modules.Reviews.Domain.Entities;

namespace MiniECommerce.Modules.Reviews.Application.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewReadDto>();
        }
    }
}
