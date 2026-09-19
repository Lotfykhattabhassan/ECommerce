using AutoMapper;
using MiniECommerce.Modules.Identity.Application.DTOs.UserRoleDto;
using MiniECommerce.Modules.Identity.Domain.Entities;

namespace MiniECommerce.Modules.Identity.Application.Profiles.UserRoleProfile
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRole, UserRoleReadDto>();
        }
    }
}
