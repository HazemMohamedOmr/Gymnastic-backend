using FluentValidation;

namespace Gymnastic.Application.UseCases.Categories.Commands.UpdateCategoryCommand
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0).WithMessage("\"Id\" must be greater than 0");
            RuleFor(c => c.Name).NotEmpty().WithMessage("\"Name\" is Required!");
        }
    }
}
