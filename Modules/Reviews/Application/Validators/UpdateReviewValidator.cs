using FluentValidation;
using MiniECommerce.Modules.Reviews.Application.DTOs;

namespace MiniECommerce.Modules.Reviews.Application.Validators
{
    public class UpdateReviewValidator :AbstractValidator<UpdateReviewDto>
    {
        public UpdateReviewValidator()
        {
            RuleFor(x => x.Id)
            .Must(x => x > 0);

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5);

            RuleFor(x => x.Comment)
                .MaximumLength(1000);
        }
    }
}
