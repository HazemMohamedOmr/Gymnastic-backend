using FluentValidation;

namespace Gymnastic.Application.UseCases.Categories.Commands.CreateCategoryCommand
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().NotNull().WithMessage($"\"Name\" is Required!");
        }
    }
}
