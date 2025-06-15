using FluentValidation;

namespace Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct.Command;

public class DeleteFinishedProductCommandValidator : AbstractValidator<DeleteFinishedProductCommand>
{
    public DeleteFinishedProductCommandValidator()
    {
        RuleFor(q => q.FinishedProductId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("FinishedProductId must not be an empty GUID.");}
}