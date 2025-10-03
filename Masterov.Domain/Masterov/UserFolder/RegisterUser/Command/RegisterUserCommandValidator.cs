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
        
        RuleFor(c => c.Password).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The password should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
        RuleFor(c => c.Phone)
            .Matches(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$")
            .WithErrorCode("InvalidPhone")
            .WithMessage("The phone number must contain from 7 to 10 digits.");
    }
}