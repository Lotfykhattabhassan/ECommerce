
using MiniECommerce.Modules.Notifications.Application.Abstractions;

namespace MiniECommerce.Modules.Notifications.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NotificationDbContext _context;
        public UnitOfWork(NotificationDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
