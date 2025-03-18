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
                .GreaterThanOrEqualTo(1).WithMessage("Quantity must be at least 1");
        }
    }
}
