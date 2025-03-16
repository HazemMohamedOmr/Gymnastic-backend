using FluentValidation;

namespace Gymnastic.Application.UseCases.Categories.Queries.GetAllCategoriesQuery
{
    public class GetAllCategoriesValidator : AbstractValidator<GetAllCategoriesQuery>
    {
        public GetAllCategoriesValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .When(x => x.PageNumber.HasValue)
                .WithMessage("Page number must be greater than or equal to 1");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .When(x => x.PageSize.HasValue)
                .WithMessage("Page size must be greater than or equal to 1");

            RuleFor(x => x.PageNumber)
                .NotNull()
                .When(x => x.PageSize.HasValue)
                .WithMessage("Page number is required when page size is specified");

            RuleFor(x => x.PageSize)
                .NotNull()
                .When(x => x.PageNumber.HasValue)
                .WithMessage("Page size is required when page number is specified");
        }
    }
}
