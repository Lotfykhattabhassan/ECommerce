using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Cart.Application.Abstractions;
using MiniECommerce.Modules.Cart.Domain.Enums;
using MiniECommerce.Modules.Cart.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Cart.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDbContext _context;

        public CartRepository(CartDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(
            Domain.Entities.Cart cart,
            CancellationToken cancellationToken = default)
        {
            if (cart == null)
                throw new ArgumentNullException(nameof(cart));

            await _context.Carts.AddAsync(cart, cancellationToken);
        }

        public async Task<Domain.Entities.Cart?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            return await _context.Carts
                .Include(x => x.CartItems)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<Domain.Entities.Cart>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            return await _context.Carts
                .Include(x => x.CartItems)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }
        public async Task<Domain.Entities.Cart?> GetActiveByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            return await _context.Carts
                .Include(x => x.CartItems)
                .FirstOrDefaultAsync(
                    x => x.UserId == userId &&
                         x.Status == CartStatus.Active,
                    cancellationToken);
        }
        public async Task<Domain.Entities.Cart?> GetByIdAndUserIdAsync(
            Guid cartId,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            if (cartId == Guid.Empty)
                throw new ArgumentException(nameof(cartId));

            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            return await _context.Carts
                .Include(x=>x.CartItems)
                .FirstOrDefaultAsync(x => x.Id == cartId &&
                x.UserId == userId,
                cancellationToken);
        }
        public async Task<bool> ExistsByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            return await _context.Carts
                .AnyAsync(
                    x => x.UserId == userId,
                    cancellationToken);
        }
    }
}