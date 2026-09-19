using FluentValidation;
using MiniECommerce.Modules.Identity.Application.DTOs.Auth;

namespace MiniECommerce.Modules.Identity.Application.Validators.AuthValidators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .MaximumLength(256);
        }
    }
}
