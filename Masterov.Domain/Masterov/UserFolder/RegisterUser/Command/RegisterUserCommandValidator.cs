using FluentValidation;

namespace Masterov.Domain.Masterov.UserFolder.RegisterUser.Command;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The login should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the login should not be more than 100.")
            .EmailAddress()
            .WithErrorCode("InvalidEmail")
            .WithMessage("The login must be a valid email address.");

        RuleFor(x => x.Password)
            .MinimumLength(6)
            .WithMessage("The new password must contain at least 6 characters.")
            .MaximumLength(100)
            .WithMessage("The new password cannot exceed 100 characters.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$")
            .WithMessage("The password must contain one uppercase letter, one lowercase letter and one digit");
        
        RuleFor(c => c.Phone)
            .Matches(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$")
            .WithErrorCode("InvalidPhone")
            .WithMessage("The phone number must contain from 7 to 10 digits.");
    }
}