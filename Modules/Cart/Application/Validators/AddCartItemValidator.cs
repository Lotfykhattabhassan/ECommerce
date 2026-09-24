using FluentValidation;
using MiniECommerce.Modules.Cart.Application.DTOs.Cart;

namespace MiniECommerce.Modules.Cart.Application.Validators.Cart
{
    public class AddCartItemValidator : AbstractValidator<AddCartItemDto>
    {
        public AddCartItemValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty();

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
