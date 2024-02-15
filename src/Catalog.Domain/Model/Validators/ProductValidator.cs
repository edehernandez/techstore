using FluentValidation;

namespace Catalog.Business.Model.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Brand).NotEmpty().WithMessage("Brand is required.");
            RuleFor(product => product.Brand).MaximumLength(50).WithMessage("Brand cannot exceed 50 characters.");

            RuleFor(product => product.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(product => product.Name).MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(product => product.Description).MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(product => product.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be non-negative.");

            RuleFor(product => product.QuantityInStock).GreaterThanOrEqualTo(0).WithMessage("Quantity in stock must be non-negative.");
        }
    }
}
