using MiniECommerce.Modules.Notifications.Application.DTOs;

namespace MiniECommerce.Modules.Notifications.Application.Abstractions
{
    public interface INotificationRealtimePublisher
    {
        Task PublishAsync(
            Guid userId,
            NotificationRealtimeDto notification);
    }
}
