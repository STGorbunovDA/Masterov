using FluentValidation;

namespace Masterov.Domain.Masterov.UserFolder.ChangePasswordFromUser.Command;

public class ChangePasswordFromUserCommandValidator : AbstractValidator<ChangePasswordFromUserCommand>
{
    public ChangePasswordFromUserCommandValidator()
    {
        RuleFor(q => q.UserId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UserId must not be an empty GUID.");
        
        RuleFor(c => c.OldPassword).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The password should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
        RuleFor(c => c.NewPassword).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The password should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
    }
}