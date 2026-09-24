
namespace MiniECommerce.Modules.Cart.Application.Abstractions
{
    public interface ICartRepository
    {
        Task AddAsync(
            Domain.Entities.Cart cart,
            CancellationToken cancellationToken = default);

        Task<Domain.Entities.Cart?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);
        Task<Domain.Entities.Cart?> GetByIdAndUserIdAsync(
            Guid cartId,
            Guid userId,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Domain.Entities.Cart>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
        Task<Domain.Entities.Cart?> GetActiveByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
        Task<bool> ExistsByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}