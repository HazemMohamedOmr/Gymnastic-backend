using FluentValidation;

namespace Gymnastic.Application.UseCases.Auth.Commands.VerifyEmailCommand
{
    public class VerifyEmailValidator : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().NotNull().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
        }
    }
}
