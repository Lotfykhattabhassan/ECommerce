using MiniECommerce.Modules.Notifications.Domain.Enums;

namespace MiniECommerce.Modules.Notifications.Application.DTOs
{
    public class NotificationRealtimeDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Message { get; set; } = null!;

        public NotificationType Type { get; set; }

        public bool IsRead { get; set; }

        public DateTime? ReadAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
