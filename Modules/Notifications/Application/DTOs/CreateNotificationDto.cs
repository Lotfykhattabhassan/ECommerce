
using MiniECommerce.Modules.Notifications.Domain.Enums;

namespace MiniECommerce.Modules.Notifications.Application.DTOs
{
    public class CreateNotificationDto
    {
        public Guid UserId { get; set; }

        public string Title { get; set; } = null!;

        public string Message { get; set; } = null!;

        public NotificationType Type { get; set; }
    }
}
