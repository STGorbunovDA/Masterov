using FluentValidation;

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
        
        RuleFor(command => command.Role).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithMessage("The specified role is invalid.");
    }
}