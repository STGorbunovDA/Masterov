using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById.Command;

public class ChangeRoleUserByIdCommandValidator : AbstractValidator<ChangeRoleUserByIdCommand>
{
    public ChangeRoleUserByIdCommandValidator()
    {
        RuleFor(q => q.UserId)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UserId must not be an empty GUID.");
        
        RuleFor(q => q.Role).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithErrorCode("InvalidRole")
            .WithMessage("Role must be a valid value")
            .Must(role => role != UserRole.Unknown)
            .WithErrorCode("InvalidRoleValue")
            .WithMessage("Role cannot be 'Unknown'");
    }
}