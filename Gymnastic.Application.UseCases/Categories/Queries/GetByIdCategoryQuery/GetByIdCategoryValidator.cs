using FluentValidation;

namespace Gymnastic.Application.UseCases.Categories.Queries.GetByIdCategoryQuery
{
    public class GetByIdCategoryValidator : AbstractValidator<GetByIdCategoryQuery>
    {
        public GetByIdCategoryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty();
        }
    }
}
