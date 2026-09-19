using AutoMapper;
using MiniECommerce.Modules.Identity.Application.DTOs.Role;
using MiniECommerce.Modules.Identity.Domain.Entities;

namespace MiniECommerce.Modules.Identity.Application.Profiles.RoleProfile
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleReadDto>();
        }
    }
}
