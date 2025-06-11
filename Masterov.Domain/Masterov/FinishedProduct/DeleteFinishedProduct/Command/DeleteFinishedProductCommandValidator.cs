using FluentValidation;

namespace Masterov.Domain.Masterov.FinishedProduct.DeleteFinishedProduct.Command;

public class DeleteFinishedProductCommandValidator : AbstractValidator<DeleteFinishedProductCommand>
{
    public DeleteFinishedProductCommandValidator()
    {
        RuleFor(q => q.FinishedProductId)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("FinishedProductId must not be an empty GUID.");}
}