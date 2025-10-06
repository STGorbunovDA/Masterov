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
        
        RuleFor(x => x.OldPassword)
            .NotEmpty()
            .WithErrorCode("OldPasswordRequired")
            .WithMessage("Old password is required when new password is provided.")
            .When(x => !string.IsNullOrEmpty(x.NewPassword));

        RuleFor(x => x.OldPassword)
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The old password cannot exceed 100 characters.");

        When(x => !string.IsNullOrEmpty(x.NewPassword), () =>
        {
            RuleFor(x => x.NewPassword)
                .MinimumLength(6)
                .WithMessage("The new password must contain at least 6 characters.")
                .MaximumLength(100)
                .WithMessage("The new password cannot exceed 100 characters.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$")
                .WithMessage("The password must contain one uppercase letter, one lowercase letter and one digit")
                .NotEqual(x => x.OldPassword)
                .WithMessage("Passwords must be different");
        });
    }
}