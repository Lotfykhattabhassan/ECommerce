
namespace MiniECommerce.Modules.Reviews.Application.DTOs
{
    public record CreateReviewDto(Guid ProductId,
           int Rating,
           string? Comment)
    {
    }
}
