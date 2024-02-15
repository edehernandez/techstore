using FluentValidation;

namespace Catalog.Business.Model.Validators
{
    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(login => login.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(login => login.Email).EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(login => login.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(login => login.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            RuleFor(login => login.Password).MaximumLength(50).WithMessage("Password cannot exceed 50 characters.");
        }
    }
}
