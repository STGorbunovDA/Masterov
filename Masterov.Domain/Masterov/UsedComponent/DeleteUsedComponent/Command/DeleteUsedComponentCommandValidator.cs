using FluentValidation;

namespace Masterov.Domain.Masterov.UsedComponent.DeleteUsedComponent.Command;

public class DeleteUsedComponentCommandValidator : AbstractValidator<DeleteUsedComponentCommand>
{
    public DeleteUsedComponentCommandValidator()
    {
        RuleFor(q => q.UsedComponentId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UsedComponentId must not be an empty GUID.");}
}