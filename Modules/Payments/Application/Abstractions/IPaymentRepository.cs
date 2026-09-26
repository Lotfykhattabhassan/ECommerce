
namespace MiniECommerce.Modules.Payment.Application.Abstractions.Persistence
{
    public interface IPaymentRepository
    {
        Task AddAsync(
            Domain.Entities.Payment payment,
            CancellationToken cancellationToken = default);

        Task<Domain.Entities.Payment?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<Domain.Entities.Payment?> GetByOrderIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Domain.Entities.Payment>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}