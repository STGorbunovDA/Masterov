using FluentValidation;

namespace Masterov.Domain.Masterov.ComponentType.DeleteComponentType.Command;

public class DeleteDeceasedCommandValidator : AbstractValidator<DeleteComponentTypeCommand>
{
    public DeleteDeceasedCommandValidator()
    {
        RuleFor(q => q.ComponentTypeId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ComponentTypeId must not be an empty GUID.");
    }
}