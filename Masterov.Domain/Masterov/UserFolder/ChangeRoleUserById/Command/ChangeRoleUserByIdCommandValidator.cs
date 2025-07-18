using FluentValidation;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUserById.Command;

public class ChangeRoleUserByIdCommandValidator : AbstractValidator<ChangeRoleUserByIdCommand>
{
    public ChangeRoleUserByIdCommandValidator()
    {
        RuleFor(q => q.UserId)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UserId must not be an empty GUID.");
        
        RuleFor(command => command.Role).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithMessage("The specified role is invalid.");
    }
}