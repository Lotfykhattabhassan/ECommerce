using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Notifications.Application.Abstractions;
using MiniECommerce.Modules.Notifications.Domain.Entities;
using MiniECommerce.Modules.Notifications.Domain.Enums;
using MiniECommerce.Modules.Notifications.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Notifications.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationDbContext _context;
        public NotificationRepository(NotificationDbContext context)
        {
            _context = context;
        }
        public async Task AddNotificationAsync(Notification notification,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(notification);

            await _context.Notifications.AddAsync(notification, cancellationToken);
        }

        public async Task<Notification?> GetNotificationByIdAsync(int id,
            CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException(nameof(id));

            return await _context.Notifications.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<IReadOnlyCollection<Notification>> GetAllNotificationsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Notifications.ToListAsync(cancellationToken);
        }
        public async Task<IReadOnlyCollection<Notification>> GetUserNotificationsAsync(Guid userId,
            CancellationToken cancellationToken = default)
        {
            if(userId == Guid.Empty) throw new ArgumentException(nameof(userId));

            return await _context.Notifications.
                 Where(x => x.UserId == userId)
                 .ToListAsync(cancellationToken);

        }
        public async Task<IReadOnlyCollection<Notification>> GetNotificationsByTypeAsync(NotificationType type,
            CancellationToken cancellationToken = default)
        {
            return await _context.Notifications.
                Where(x => x.Type == type)
                .ToListAsync(cancellationToken);
        }

    }
}
