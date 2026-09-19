using FluentValidation;
using MiniECommerce.Modules.Identity.Application.DTOs.Role;

namespace MiniECommerce.Modules.Identity.Application.Validators.RoleValidators
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleDto>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty()
                .Must(x => x > 0);

            RuleFor(x => x.NewRoleName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.NewDescription)
                .NotEmpty()
                .MaximumLength(1500)
                .MinimumLength(50);
        }
    }
}
