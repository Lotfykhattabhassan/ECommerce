using FluentValidation;
using MiniECommerce.Modules.Inventory.Application.DTOs;

namespace MiniECommerce.Modules.Inventory.Application.Validators
{
    public class ReserveStockValidator : AbstractValidator<ReserveStockDto>
    {
        public ReserveStockValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
