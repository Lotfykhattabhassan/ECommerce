
namespace MiniECommerce.Modules.Reviews.Application.DTOs
{
    public record ReviewReadDto(int Id,
        Guid ProductId,
            Guid UserId,
           int Rating,
           string? Comment);
    
}
