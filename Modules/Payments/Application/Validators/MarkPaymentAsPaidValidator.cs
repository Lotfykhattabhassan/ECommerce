using FluentValidation;
using MiniECommerce.Modules.Payment.Application.DTOs;

namespace MiniECommerce.Modules.Payment.Application.Validators
{
    public class MarkPaymentAsPaidValidator
        : AbstractValidator<MarkPaymentAsPaidDto>
    {
        public MarkPaymentAsPaidValidator()
        {
            RuleFor(x => x.TransactionId)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}