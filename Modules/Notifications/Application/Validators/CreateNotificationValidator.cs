using FluentValidation;
using MiniECommerce.Modules.Notifications.Application.DTOs;
namespace MiniECommerce.Modules.Notifications.Application.Validators
{
    public class CreateNotificationValidator : AbstractValidator<CreateNotificationDto>
    {
        public CreateNotificationValidator()
        {
            RuleFor(x => x.UserId)
                .Must(x => x != Guid.Empty);

            RuleFor(x => x.Message)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Type)
                .IsInEnum();
        }
    }
}
