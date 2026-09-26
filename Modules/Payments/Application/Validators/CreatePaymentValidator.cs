using FluentValidation;
using MiniECommerce.Modules.Payment.Application.DTOs;

namespace MiniECommerce.Modules.Payment.Application.Validators
{
    public class CreatePaymentValidator
        : AbstractValidator<CreatePaymentDto>
    {
        public CreatePaymentValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty();

            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Amount)
                .GreaterThan(0);

            RuleFor(x => x.Method)
                .IsInEnum();
        }
    }
}