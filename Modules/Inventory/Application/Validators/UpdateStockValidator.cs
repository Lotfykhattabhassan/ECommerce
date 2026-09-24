using FluentValidation;
using MiniECommerce.Modules.Inventory.Application.DTOs;

namespace MiniECommerce.Modules.Inventory.Application.Validators
{
    public class UpdateStockValidator : AbstractValidator<UpdateStockDto>
    {
        public UpdateStockValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
