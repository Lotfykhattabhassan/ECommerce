using FluentValidation;
using MiniECommerce.Modules.Catalog.Application.DTOs.Category;

namespace MiniECommerce.Modules.Catalog.Application.Validators.Category
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(1000);

            RuleFor(x => x.ParentCategoryId)
                .NotEqual(Guid.Empty)
                .When(x => x.ParentCategoryId.HasValue);
        }
    }
}
