using FluentValidation;
using MiniECommerce.Modules.Identity.Application.DTOs.User;

namespace MiniECommerce.Modules.Identity.Application.Validators.UserValidators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(x => x != Guid.Empty);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
               .NotEmpty()
               .MaximumLength(50);
        }
    }
}
