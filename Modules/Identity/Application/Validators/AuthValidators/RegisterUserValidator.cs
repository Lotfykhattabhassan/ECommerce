using FluentValidation;
using MiniECommerce.Modules.Identity.Application.DTOs.Auth;

namespace MiniECommerce.Modules.Identity.Application.Validators.AuthValidators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
               .NotEmpty()
               .MaximumLength(50);

            RuleFor(x => x.Email)
               .NotEmpty()
               .EmailAddress()
               .MaximumLength(256);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(256);


            RuleFor(x => x.RoleId)
                .NotEmpty()
                .Must(x => x > 0);
        }
    }
}
