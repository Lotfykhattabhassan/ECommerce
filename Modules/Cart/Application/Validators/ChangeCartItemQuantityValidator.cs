using FluentValidation;
using MiniECommerce.Modules.Cart.Application.DTOs.Cart;

namespace MiniECommerce.Modules.Cart.Application.Validators
{
    public class ChangeCartItemQuantityValidator : AbstractValidator<ChangeCartItemQuantityDto>
    {
        public ChangeCartItemQuantityValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
