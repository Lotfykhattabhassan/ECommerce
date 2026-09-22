
using MiniECommerce.Modules.Notifications.Domain.Enums;

namespace MiniECommerce.Modules.Notifications.Application.DTOs
{
    public class CreateNotificationDto
    {
        public Guid UserId { get; private set; }

        public string Title { get; private set; } = null!;

        public string Message { get; private set; } = null!;

        public NotificationType Type { get; private set; }
    }
}
