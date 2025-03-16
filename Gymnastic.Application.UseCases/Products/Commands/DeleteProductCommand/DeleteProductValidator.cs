using FluentValidation;

namespace Gymnastic.Application.UseCases.Products.Commands.DeleteProductCommand
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0).WithMessage("Invalid Id");
        }
    }
}
