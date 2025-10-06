using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUserByLogin.Command;

public class ChangeRoleUserByLoginCommandValidator : AbstractValidator<ChangeRoleUserByLoginCommand>
{
    public ChangeRoleUserByLoginCommandValidator()
    {
        RuleFor(c => c.Login).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The login should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
        RuleFor(q => q.Role).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithErrorCode("InvalidRole")
            .WithMessage("Role must be a valid value")
            .Must(role => role != UserRole.Unknown)
            .WithErrorCode("InvalidRoleValue")
            .WithMessage("Role cannot be 'Unknown'");
    }
}