using FluentValidation;

namespace Gymnastic.Application.UseCases.Auth.Commands.SendEmailVerificationCommand
{
    internal class SendEmailVerificationValidator : AbstractValidator<SendEmailVerificationCommand>
    {
        public SendEmailVerificationValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
        }
    }
}
