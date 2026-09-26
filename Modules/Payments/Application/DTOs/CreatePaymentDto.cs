using MiniECommerce.Modules.Payment.Domain.Enums;

namespace MiniECommerce.Modules.Payment.Application.DTOs
{
    public record CreatePaymentDto(
        Guid OrderId,
        Guid UserId,
        decimal Amount,
        PaymentMethod Method);
}