using FluentValidation;

namespace Masterov.Domain.Masterov.UserFolder.DeleteUserByLogin.Command;

public class DeleteUserByLoginCommandValidator : AbstractValidator<DeleteUserByLoginCommand>
{
    public DeleteUserByLoginCommandValidator()
    {
        RuleFor(c => c.Login).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The login should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
    }
}