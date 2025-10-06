using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.UserFolder.UpdateUser.Command;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(q => q.UserId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UserId must not be an empty GUID.");
        
        RuleFor(c => c.CustomerId)
            .Must(id => !id.HasValue || id != Guid.Empty)
            .WithErrorCode("InvalidCustomerId")
            .WithMessage("CustomerId must not be an empty GUID when provided.");
        
        RuleFor(c => c.Login).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .EmailAddress()
            .WithMessage("The login must be a valid email")
            .WithMessage("The login should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the login should not be more than 100");
        
        RuleFor(q => q.Role).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithErrorCode("InvalidRole")
            .WithMessage("Role must be a valid value")
            .Must(role => role != UserRole.Unknown)
            .WithErrorCode("InvalidRoleValue")
            .WithMessage("Role cannot be 'Unknown'");
        
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
        
        // Валидация даты создания (CreatedAt)
        RuleFor(c => c.CreatedAt)
            .Must(DomainExtension.BeValidPastOrPresentDate)
            .When(c => c.CreatedAt.HasValue)
            .WithErrorCode("InvalidCreatedAt")
            .WithMessage("Creation date cannot be in the future.");
        
    }
}