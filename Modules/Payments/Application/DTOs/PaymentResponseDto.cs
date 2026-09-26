using MiniECommerce.Modules.Payment.Domain.Enums;

namespace MiniECommerce.Modules.Payment.Application.DTOs
{
    public record PaymentResponseDto(
        Guid Id,
        Guid OrderId,
        Guid UserId,
        decimal Amount,
        PaymentMethod Method,
        PaymentStatus Status,
        string? TransactionId,
        DateTime CreatedAt,
        DateTime? PaidAt);
}