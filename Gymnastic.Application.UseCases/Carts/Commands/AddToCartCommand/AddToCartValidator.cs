using FluentValidation;

namespace Gymnastic.Application.UseCases.Carts.Commands.AddToCartCommand
{
    public class AddToCartValidator : AbstractValidator<AddToCartCommand>
    {
        public AddToCartValidator()
        {
            RuleFor(c => c.CartId)
                .NotNull().NotEmpty().WithMessage("Invalid Cart!");

            RuleFor(c => c.ProductId)
            .NotNull().NotEmpty().WithMessage("Invalid Product");

            RuleFor(c => c.Quantity)
                .NotNull().NotEmpty().WithMessage("Quantity must be specified!")
                .Must(q => q >= 1 || q <= 5).WithMessage("Quantity must be between 1 to 5.");
        }
    }
}
