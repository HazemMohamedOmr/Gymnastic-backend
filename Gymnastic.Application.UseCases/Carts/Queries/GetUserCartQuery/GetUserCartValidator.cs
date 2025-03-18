using FluentValidation;

namespace Gymnastic.Application.UseCases.Carts.Queries.GetUserCartQuery
{
    public class GetUserCartValidator : AbstractValidator<GetUserCartQuery>
    {
        public GetUserCartValidator()
        {
            //RuleFor(c => c.UserId)
            //    .NotEmpty().NotNull().WithMessage("User ID is required");
        }
    }
}
