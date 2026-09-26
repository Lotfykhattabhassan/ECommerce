using MiniECommerce.Modules.Payment.Application.DTOs;

namespace MiniECommerce.Modules.Payment.Application.Abstractions
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> CreateAsync(
            CreatePaymentDto dto,
            CancellationToken cancellationToken = default);

        Task<PaymentResponseDto?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<PaymentResponseDto?> GetByOrderIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<PaymentResponseDto>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<PaymentResponseDto> MarkAsProcessingAsync(
            Guid paymentId,
            CancellationToken cancellationToken = default);

        Task<PaymentResponseDto> MarkAsPaidAsync(
            Guid paymentId,
            MarkPaymentAsPaidDto dto,
            CancellationToken cancellationToken = default);

        Task<PaymentResponseDto> MarkAsFailedAsync(
            Guid paymentId,
            CancellationToken cancellationToken = default);

        Task<PaymentResponseDto> CancelAsync(
            Guid paymentId,
            CancellationToken cancellationToken = default);
        Task<PaymentResponseDto> DeleteAsync(
            Guid paymentId,
            CancellationToken cancellationToken = default);
    }
}