using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Payment.Application.Abstractions.Persistence;
using MiniECommerce.Modules.Payment.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Payment.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(
            Domain.Entities.Payment payment,
            CancellationToken cancellationToken = default)
        {
            await _context.Payments.AddAsync(
                payment,
                cancellationToken);
        }

        public async Task<Domain.Entities.Payment?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<Domain.Entities.Payment?> GetByOrderIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(
                    x => x.OrderId == orderId,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<Domain.Entities.Payment>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Payments
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}