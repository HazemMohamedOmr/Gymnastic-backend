using FluentValidation;
using Gymnastic.Domain.Constants;

namespace Gymnastic.Application.UseCases.Auth.Commands.RegisterCommand
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        private static readonly string[] ValidRoles =
        [
            RoleConstants.Admin,
            RoleConstants.Customer,
            RoleConstants.Coach
        ];

        public RegisterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().NotNull().WithMessage("First name is required")
                .Length(2, 50).WithMessage("First name must be between 2 and 50 characters")
                .Matches(@"^[a-zA-Z\s-]+$").WithMessage("First name can only contain letters, spaces, or hyphens");

            RuleFor(x => x.LastName)
                .NotEmpty().NotNull().WithMessage("Last name is required")
                .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters")
                .Matches(@"^[a-zA-Z\s-]+$").WithMessage("Last name can only contain letters, spaces, or hyphens");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(100).WithMessage("Email must not exceed 100 characters");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().NotNull().WithMessage("Phone number is required")
                .Matches(@"^\+?[0-9]\d{1,14}$").WithMessage("Invalid phone number format")
                .Length(8, 15).WithMessage("Phone number must be between 8 and 15 digits");

            RuleFor(x => x.Password)
                .NotEmpty().NotNull().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .MaximumLength(100).WithMessage("Password must not exceed 100 characters")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number")
                .Matches(@"[!@#$%^&*]").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.Role)
                .NotEmpty().NotNull().WithMessage("Role is required")
                .Must(role => ValidRoles.Contains(role.ToLower()))
                .WithMessage($"Role is invalid");
        }
    }
}
