using Catalog.Business.Model;
using FluentValidation.TestHelper;
using Catalog.Business.Model.Validators;

namespace Catalog.Domain.Unit.Tests.Validators
{
    [TestFixture]
    public class ProductValidatorTests
    {
        private ProductValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new ProductValidator();
        }

        [Test]
        public void Brand_Should_Not_Be_Empty()
        {
            // Arrange
            var product = new Product { Brand = null };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Brand)
                  .WithErrorMessage("Brand is required.");
        }

        [Test]
        public void Brand_Should_Not_Exceed_Maximum_Length()
        {
            // Arrange
            var product = new Product { Brand = new string('x', 51) };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Brand)
                  .WithErrorMessage("Brand cannot exceed 50 characters.");
        }

        [Test]
        public void Name_Should_Not_Be_Empty()
        {
            // Arrange
            var product = new Product { Name = null };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                  .WithErrorMessage("Name is required.");
        }

        [Test]
        public void Name_Should_Not_Exceed_Maximum_Length()
        {
            // Arrange
            var product = new Product { Name = new string('x', 101) };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                  .WithErrorMessage("Name cannot exceed 100 characters.");
        }

        [Test]
        public void Description_Should_Not_Exceed_Maximum_Length()
        {
            // Arrange
            var product = new Product { Description = new string('x', 501) };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description)
                  .WithErrorMessage("Description cannot exceed 500 characters.");
        }

        [Test]
        public void Price_Should_Be_Non_Negative()
        {
            // Arrange
            var product = new Product { Price = -1 };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Price)
                  .WithErrorMessage("Price must be non-negative.");
        }

        [Test]
        public void QuantityInStock_Should_Be_Non_Negative()
        {
            // Arrange
            var product = new Product { QuantityInStock = -1 };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.QuantityInStock)
                  .WithErrorMessage("Quantity in stock must be non-negative.");
        }
    }
}