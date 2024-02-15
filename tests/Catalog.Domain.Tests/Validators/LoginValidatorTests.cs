using Catalog.Business.Model;
using FluentValidation.TestHelper;
using Catalog.Business.Model.Validators;

namespace Catalog.Domain.Unit.Tests.Validators
{
    [TestFixture]
    public class LoginValidatorTests
    {
        private LoginValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new LoginValidator();
        }

        [Test]
        public void Email_Should_Not_Be_Empty()
        {
            // Arrange
            var login = new Login { Email = null, Password = "password" };

            // Act
            var result = _validator.TestValidate(login);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage("Email is required.");
        }

        [Test]
        public void Email_Should_Have_Valid_Format()
        {
            // Arrange
            var login = new Login { Email = "invalidemail", Password = "password" };

            // Act
            var result = _validator.TestValidate(login);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage("Invalid email address format.");
        }

        [Test]
        public void Password_Should_Not_Be_Empty()
        {
            // Arrange
            var login = new Login { Email = "test@example.com", Password = null };

            // Act
            var result = _validator.TestValidate(login);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password)
                  .WithErrorMessage("Password is required.");
        }

        [Test]
        public void Password_Length_Should_Be_At_Least_Six_Characters_Long()
        {
            // Arrange
            var login = new Login { Email = "test@example.com", Password = "pass" };

            // Act
            var result = _validator.TestValidate(login);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password)
                  .WithErrorMessage("Password must be at least 6 characters long.");
        }

        [Test]
        public void Password_Should_Not_Exceed_Fifty_Characters_Long()
        {
            // Arrange
            var longPassword = new string('a', 51); // Password length of 51 characters
            var login = new Login { Email = "test@example.com", Password = longPassword };

            // Act
            var result = _validator.TestValidate(login);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password)
                  .WithErrorMessage("Password cannot exceed 50 characters.");
        }

        [Test]
        public void Valid_Email_And_Password_Should_Pass_Validation()
        {
            // Arrange
            var login = new Login { Email = "test@example.com", Password = "password" };

            // Act
            var result = _validator.TestValidate(login);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}