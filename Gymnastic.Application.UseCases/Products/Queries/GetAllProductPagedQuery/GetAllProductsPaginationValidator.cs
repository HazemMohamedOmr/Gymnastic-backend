using FluentValidation;

namespace Gymnastic.Application.UseCases.Products.Queries.GetAllProductPagedQuery
{
    public class GetAllProductsPaginationValidator : AbstractValidator<GetAllProductsPaginationQuery>
    {
        public GetAllProductsPaginationValidator()
        {
            RuleFor(x => x.PageNumber)
                .NotNull().WithMessage("Page number is required.")
                .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .NotNull().WithMessage("Page size is required.")
                .GreaterThan(0).WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Page size cannot exceed 100.");
        }
    }
}
