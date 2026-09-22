using MiniECommerce.BuildingBlocks.Domain.Common;
using MiniECommerce.Modules.Notifications.Domain.Enums;

namespace MiniECommerce.Modules.Notifications.Domain.Entities
{
    public class Notification : Entity<int>
    {
        public Guid UserId { get; private set; }

        public string Title { get; private set; }

        public string Message { get; private set; }

        public NotificationType Type { get; private set; }

        public bool IsRead { get; private set; }

        public DateTime? ReadAt { get; private set; }


        private Notification()
        {
        }
        private Notification(Guid userId,
            string title,
            string message,
            NotificationType type)
        {
            if (userId == Guid.Empty) throw new ArgumentException(nameof(userId));
            UserId = userId;

            if(string.IsNullOrWhiteSpace(title)) throw new ArgumentException(nameof(title));
            Title = title;

            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException(nameof(message));
            Message = message;

            Type = type;

            IsRead = false;
            CreatedAt = DateTime.UtcNow;
        }


        public static Notification Create(
            Guid userId,
            string title,
            string message,
            NotificationType type)
        {
            return new Notification(userId, title, message, type);
            
        }


        public void MarkAsRead()
        {
            if (IsRead)
                return;

            IsRead = true;
            ReadAt = DateTime.UtcNow;
        }


        public void MarkAsUnread()
        {
            if (!IsRead)
                return;

            IsRead = false;
            ReadAt = null;
        }
    }
}
