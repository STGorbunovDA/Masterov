using FluentValidation;

namespace Masterov.Domain.Masterov.UserFolder.DeleteUserById.Command;

public class DeleteUserByIdCommandValidator : AbstractValidator<DeleteUserByIdCommand>
{
    public DeleteUserByIdCommandValidator()
    {
        RuleFor(q => q.UserId)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UserId must not be an empty GUID.");
    }
}