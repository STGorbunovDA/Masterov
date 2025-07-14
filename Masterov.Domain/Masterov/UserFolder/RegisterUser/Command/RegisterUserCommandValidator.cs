using FluentValidation;

namespace Masterov.Domain.Masterov.UserFolder.RegisterUser.Command;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Login).Cascade(CascadeMode.Stop)
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
            .WithMessage("The maximum length of the name should not be more than 50");
    }
}