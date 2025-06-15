using FluentValidation;

namespace Masterov.Domain.Masterov.UserFolder.ChangeRoleUser.Command;

public class ChangeRoleUserCommandValidator : AbstractValidator<ChangeRoleUserCommand>
{
    public ChangeRoleUserCommandValidator()
    {
        RuleFor(c => c.Login).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The login should not be empty.")
            .MaximumLength(20)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 20");
        
        RuleFor(command => command.Role).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithMessage("The specified role is invalid.");
    }
}