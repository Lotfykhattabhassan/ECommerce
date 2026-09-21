using MiniECommerce.Modules.Reviews.Domain.Entities;

namespace MiniECommerce.Modules.Reviews.Application.Abstractions
{
    public interface IReviewRepository
    {
        Task AddReviewAsync(Review review,CancellationToken cancellationToken =default);
        Task<Review?> GetReviewByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Review>> GetUserReviewsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Review>> GetProductReviewsAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<Review?> GetUserProductReviewAsync(Guid userId,
            Guid productId,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Review>> GetAllReviewsAsync(CancellationToken cancellationToken = default);
    }
}
