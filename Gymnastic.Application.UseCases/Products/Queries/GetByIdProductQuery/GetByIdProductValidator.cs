using FluentValidation;

namespace Gymnastic.Application.UseCases.Products.Queries.GetByIdProductQuery
{
    public class GetByIdProductValidator : AbstractValidator<GetByIdProductQuery>
    {
        public GetByIdProductValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty();
        }
    }
}
