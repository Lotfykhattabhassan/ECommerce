using FluentValidation;
using MiniECommerce.Modules.Identity.Application.DTOs.Auth;

namespace MiniECommerce.Modules.Identity.Application.Validators.AuthValidators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
              .NotEmpty()
              .EmailAddress()
              .MaximumLength(256);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(256);
        }
    }
}
