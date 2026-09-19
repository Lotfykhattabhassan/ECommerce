using FluentValidation;
using MiniECommerce.Modules.Identity.Application.DTOs.User;
namespace MiniECommerce.Modules.Identity.Application.Validators.UserValidators
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.firstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.lastName)
               .NotEmpty()
               .MaximumLength(50);

            RuleFor(x => x.email)
               .NotEmpty()
               .EmailAddress()
               .MaximumLength(256);
        }
    }
}
