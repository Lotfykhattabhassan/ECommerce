using FluentValidation;
using MiniECommerce.Modules.Identity.Application.DTOs.Role;

namespace MiniECommerce.Modules.Identity.Application.Validators.RoleValidators
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1500)
                .MinimumLength(50);
        }
    }
}
