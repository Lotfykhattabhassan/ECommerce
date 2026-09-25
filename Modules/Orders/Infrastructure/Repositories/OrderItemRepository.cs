using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Orders.Application.Abstractions;
using MiniECommerce.Modules.Orders.Domain.Entities;
using MiniECommerce.Modules.Orders.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Orders.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly OrderDbContext _context;

        public OrderItemRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<OrderItem?> GetByIdAsync(
            Guid orderItemId,
            CancellationToken cancellationToken = default)
        {
            if (orderItemId == Guid.Empty)
                throw new ArgumentException(nameof(orderItemId));

            return await _context.OrderItems
                .FirstOrDefaultAsync(
                    x => x.Id == orderItemId,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<OrderItem>> GetByOrderIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException(nameof(orderId));

            return await _context.OrderItems
                .Where(x => x.OrderId == orderId)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(
            OrderItem orderItem,
            CancellationToken cancellationToken = default)
        {
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem));

            await _context.OrderItems.AddAsync(
                orderItem,
                cancellationToken);
        }

        public Task DeleteAsync(
            OrderItem orderItem,
            CancellationToken cancellationToken = default)
        {
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem));

            _context.OrderItems.Remove(orderItem);

            return Task.CompletedTask;
        }
    }
}