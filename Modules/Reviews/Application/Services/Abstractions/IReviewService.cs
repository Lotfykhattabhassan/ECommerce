
using MiniECommerce.Modules.Reviews.Application.DTOs;

namespace MiniECommerce.Modules.Reviews.Application.Services.Abstractions
{
    public interface IReviewService
    {
        Task<int> CreateReviewAsync(CreateReviewDto dto, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ReviewReadDto>> GetAllReviewsAsync(CancellationToken cancellationToken = default);
        Task<ReviewReadDto?> GetReviewByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ReviewReadDto>> GetMyReviewsAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ReviewReadDto>> GetUserReviewsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ReviewReadDto>> GetProductReviewsAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<string> UpdateReviewAsync(
            UpdateReviewDto dto,
            CancellationToken cancellationToken = default);
        Task<string> DeleteReviewAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
