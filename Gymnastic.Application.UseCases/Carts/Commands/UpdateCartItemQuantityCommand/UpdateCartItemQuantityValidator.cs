using FluentValidation;

namespace Gymnastic.Application.UseCases.Carts.Commands.UpdateCartItemQuantityCommand
{
    public class UpdateCartItemQuantityValidator : AbstractValidator<UpdateCartItemQuantityCommand>
    {
        public UpdateCartItemQuantityValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.Delta)
                .Must(delta => delta == 1 || delta == -1).WithMessage("Increament or Decrement only by 1 at time");
        }
    }
}
