using MiniECommerce.Modules.Notifications.Domain.Entities;
using MiniECommerce.Modules.Notifications.Domain.Enums;

namespace MiniECommerce.Modules.Notifications.Application.Abstractions
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(Notification notification, CancellationToken cancellationToken = default);
        Task<Notification?> GetNotificationByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<Notification>> GetAllNotificationsAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<Notification>> GetUserNotificationsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<Notification>> GetNotificationsByTypeAsync(NotificationType type, CancellationToken cancellationToken = default);
    }
}
