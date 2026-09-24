using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Cart.Application.Abstractions;
using MiniECommerce.Modules.Cart.Domain.Entities;
using MiniECommerce.Modules.Cart.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Cart.Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly CartDbContext _context;

        public CartItemRepository(CartDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(
            CartItem cartItem,
            CancellationToken cancellationToken = default)
        {
            if (cartItem == null)
                throw new ArgumentNullException(nameof(cartItem));

            await _context.CartItems.AddAsync(
                cartItem,
                cancellationToken);
        }

        public async Task<CartItem?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            return await _context.CartItems
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<CartItem?> GetByCartIdAndProductIdAsync(
            Guid cartId,
            Guid productId,
            CancellationToken cancellationToken = default)
        {
            if (cartId == Guid.Empty)
                throw new ArgumentException(nameof(cartId));

            if (productId == Guid.Empty)
                throw new ArgumentException(nameof(productId));

            return await _context.CartItems
                .FirstOrDefaultAsync(
                    x => x.CartId == cartId &&
                         x.ProductId == productId,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<CartItem>> GetByCartIdAsync(
            Guid cartId,
            CancellationToken cancellationToken = default)
        {
            if (cartId == Guid.Empty)
                throw new ArgumentException(nameof(cartId));

            return await _context.CartItems
                .Where(x => x.CartId == cartId)
                .ToListAsync(cancellationToken);
        }
    }
}