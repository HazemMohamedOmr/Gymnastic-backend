using FluentValidation;

namespace Gymnastic.Application.UseCases.Products.Commands.CreateProductCommand
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Product.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters")
                .WithState(x => "Name");

            RuleFor(x => x.Product.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .WithState(x => "Price");

            RuleFor(x => x.Product.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative")
                .WithState(x => "Stock");

            RuleFor(x => x.Product.CategoryId)
                .GreaterThan(0).WithMessage("Valid Category is required")
                .WithState(x => "CategoryId");

            When(x => x.Product.Images is not null, () =>
            {
                RuleFor(x => x.Product.Images)
                    .Must(images => images!.All(img =>
                        img.Length > 0 &&
                        img.Length <= 5 * 1024 * 1024 && // 5MB max
                        new[] { "image/jpeg", "image/png", "image/jpg" }.Contains(img.ContentType)))
                    .WithMessage("Images must be valid JPEG, JPG or PNG files under 5MB")
                    .WithState(x => "Images");
            });
        }
    }
}
