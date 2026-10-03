using Microsoft.AspNetCore.SignalR;
using MiniECommerce.Modules.Notifications.API.Hubs;
using MiniECommerce.Modules.Notifications.Application.Abstractions;
using MiniECommerce.Modules.Notifications.Application.DTOs;

namespace MiniECommerce.Modules.Notifications.API.Realtime
{
    public  class NotificationRealtimePublisher : INotificationRealtimePublisher
    {
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        public NotificationRealtimePublisher(
            IHubContext<NotificationHub> notificationHubContext)
        {
            _notificationHubContext = notificationHubContext;
        }
        public async Task PublishAsync(Guid userId, NotificationRealtimeDto notification)
        {
            await _notificationHubContext
                .Clients
                .User(userId.ToString())
                .SendAsync("NotificationReceived", notification);
        }
    }
}
