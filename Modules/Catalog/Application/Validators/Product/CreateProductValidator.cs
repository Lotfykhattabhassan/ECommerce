using FluentValidation;
using MiniECommerce.Modules.Catalog.Application.DTOs.Product;
namespace MiniECommerce.Modules.Catalog.Application.Validators.Product
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Product name is required.")
                .MaximumLength(100)
                .WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Product description must not exceed 500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Product price must be greater than zero.");

            RuleFor(x => x.SKU)
                .NotEmpty()
                .WithMessage("SKU is required.")
                .MaximumLength(50)
                .WithMessage("SKU must not exceed 50 characters.");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Category is required.");
        }
    }
}
