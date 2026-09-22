using MiniECommerce.Modules.Notifications.Application.DTOs;
using MiniECommerce.Modules.Notifications.Domain.Enums;

namespace MiniECommerce.Modules.Notifications.Application.Services.Abstraction
{
    public interface INotificationService
    {
        Task<int> CreateNotificationAsync(
            CreateNotificationDto dto,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<NotificationReadDto>> GetAllNotificationsAsync(
            CancellationToken cancellationToken = default);

        Task<NotificationReadDto> GetNotificationByIdAsync(
            int id,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<NotificationReadDto>> GetNotificationsByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<NotificationReadDto>> GetMyNotificationsAsync(
            CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<NotificationReadDto>> GetNotificationsByTypeAsync(
            NotificationType type,
            CancellationToken cancellationToken = default);
        Task MarkNotificationAsReadAsync(
            int id,
            CancellationToken cancellationToken = default);
        Task DeleteNotificationAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
