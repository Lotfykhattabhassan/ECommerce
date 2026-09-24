using FluentValidation;
using MiniECommerce.Modules.Inventory.Application.DTOs;

namespace MiniECommerce.Modules.Inventory.Application.Validators
{
    public class CreateInventoryValidator : AbstractValidator<CreateInventoryDto>
    {
        public CreateInventoryValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0);
        }
    }
}
