using FluentValidation;

namespace Gymnastic.Application.UseCases.Products.Queries.GetAllProductsQuery
{
    public class GetAllProductsValidator : AbstractValidator<GetAllProductsQuery>
    {
        public GetAllProductsValidator()
        {
            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinPrice.HasValue)
                .WithMessage("Minimum price must be greater than or equal to 0");

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MaxPrice.HasValue)
                .WithMessage("Maximum price must be greater than or equal to 0");

            RuleFor(x => x.pageNumber)
                .GreaterThanOrEqualTo(1)
                .When(x => x.pageNumber.HasValue)
                .WithMessage("Page number must be greater than or equal to 1");

            RuleFor(x => x.pageSize)
                .GreaterThanOrEqualTo(1)
                .When(x => x.pageSize.HasValue)
                .WithMessage("Page size must be greater than or equal to 1");

            RuleFor(x => x.pageNumber)
                .NotNull()
                .When(x => x.pageSize.HasValue)
                .WithMessage("Page number is required when page size is specified");

            RuleFor(x => x.pageSize)
                .NotNull()
                .When(x => x.pageNumber.HasValue)
                .WithMessage("Page size is required when page number is specified");

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(x => x.MinPrice.Value)
                .When(x => x.MinPrice.HasValue && x.MaxPrice.HasValue)
                .WithMessage("Maximum price must be greater than or equal to minimum price");
        }
    }
}
