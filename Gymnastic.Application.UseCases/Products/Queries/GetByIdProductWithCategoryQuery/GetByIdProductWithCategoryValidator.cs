using FluentValidation;

namespace Gymnastic.Application.UseCases.Products.Queries.GetByIdProductWithCategoryQuery
{
    public class GetByIdProductWithCategoryValidator : AbstractValidator<GetByIdProductWithCategoryQuery>
    {
        public GetByIdProductWithCategoryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty();
        }
    }
}
