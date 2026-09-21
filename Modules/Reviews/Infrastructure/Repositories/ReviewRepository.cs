using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Reviews.Application.Abstractions;
using MiniECommerce.Modules.Reviews.Domain.Entities;
using MiniECommerce.Modules.Reviews.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Reviews.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ReviewDbContext _context;
        public ReviewRepository(ReviewDbContext context)
        {
            _context = context;
        }

        public async Task AddReviewAsync(Review review, CancellationToken cancellationToken = default)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            await _context.Reviews.AddAsync(review, cancellationToken);
        }

        public async Task<Review?> GetReviewByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if(id <= 0) throw new ArgumentException(nameof(id));

            return await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Review>> GetUserReviewsAsync(Guid userId,
            CancellationToken cancellationToken = default)
        {
            if(userId == Guid.Empty) throw new ArgumentException(nameof(userId));

            return await _context.Reviews.
                Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Review>> GetProductReviewsAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            if (productId == Guid.Empty) throw new ArgumentException(nameof(productId));

            return await _context.Reviews.
                Where(x => x.ProductId == productId)
                .ToListAsync(cancellationToken);
        }
        public async Task<Review?> GetUserProductReviewAsync(Guid userId,
            Guid productId,
            CancellationToken cancellationToken = default)
        {
            if (productId == Guid.Empty) throw new ArgumentException(nameof(productId));
            if (userId == Guid.Empty) throw new ArgumentException(nameof(userId));

            return await _context.Reviews.FirstOrDefaultAsync(x => x.UserId == userId 
            && x.ProductId == productId , cancellationToken);

        }

        public async Task<IReadOnlyList<Review>> GetAllReviewsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Reviews.ToListAsync(cancellationToken);
        }

    }
}
