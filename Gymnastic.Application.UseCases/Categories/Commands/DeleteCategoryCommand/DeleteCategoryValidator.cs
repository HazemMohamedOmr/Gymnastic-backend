using FluentValidation;

namespace Gymnastic.Application.UseCases.Categories.Commands.DeleteCategoryCommand
{
    public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0).WithMessage("Invalid Id");
        }
    }
}
