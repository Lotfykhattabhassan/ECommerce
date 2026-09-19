using AutoMapper;
using MiniECommerce.Modules.Identity.Application.DTOs.User;
using MiniECommerce.Modules.Identity.Domain.Entities;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserReadDto>()
            .ForMember(
                dst => dst.FullName,
                opt => opt.MapFrom(
                    src => $"{src.FirstName} {src.LastName}"
                )
            )
            .ForMember(
                dst => dst.HasCredentials,
                opt => opt.MapFrom(
                    src => src.Credential != null
                )
            )
            .ForMember(dst => dst.Roles,
            opt => opt.MapFrom(
                src => src.Roles.Select(x=>x.Role.RoleName)
                )
            )
            .ForMember(dst => dst.Status,
            opt => opt.MapFrom(
                src => src.Status.ToString()));
    }
}