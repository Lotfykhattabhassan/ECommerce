using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Orders.Application.Abstractions;
using MiniECommerce.Modules.Orders.Domain.Entities;
using MiniECommerce.Modules.Orders.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Orders.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(
            Order order,
            CancellationToken cancellationToken = default)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            await _context.Orders.AddAsync(
                order,
                cancellationToken);
        }

        public async Task<Order?> GetByIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException(nameof(orderId));

            return await _context.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(
                    x => x.Id == orderId,
                    cancellationToken);
        }

        public async Task<Order?> GetOrderByIdAndUserIdAsync(
            Guid orderId,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException(nameof(orderId));

            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            return await _context.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(
                    x => x.Id == orderId &&
                         x.UserId == userId,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<Order>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            return await _context.Orders
                .Include(x => x.OrderItems)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}