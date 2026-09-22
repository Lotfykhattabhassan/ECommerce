using AutoMapper;
using MiniECommerce.Modules.Notifications.Application.DTOs;
using MiniECommerce.Modules.Notifications.Domain.Entities;
namespace MiniECommerce.Modules.Notifications.Application.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationReadDto>();
        }
    }
}
