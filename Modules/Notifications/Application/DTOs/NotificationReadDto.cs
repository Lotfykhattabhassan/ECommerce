
using MiniECommerce.Modules.Notifications.Domain.Enums;

namespace MiniECommerce.Modules.Notifications.Application.DTOs
{
    public class NotificationReadDto
    {
        public int Id { get; set; }
        public Guid UserId { get; private set; }

        public string Title { get; private set; } = null!;

        public string Message { get; private set; } = null!;

        public NotificationType Type { get; private set; }

        public bool IsRead { get; private set; }

        public DateTime? ReadAt { get; private set; }
    }
}
