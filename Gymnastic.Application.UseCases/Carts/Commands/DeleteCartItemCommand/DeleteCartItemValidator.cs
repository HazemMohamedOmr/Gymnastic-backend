using FluentValidation;

namespace Gymnastic.Application.UseCases.Carts.Commands.DeleteCartItemCommand
{
    public class DeleteCartItemValidator : AbstractValidator<DeleteCartItemCommand>
    {
        public DeleteCartItemValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }
}
