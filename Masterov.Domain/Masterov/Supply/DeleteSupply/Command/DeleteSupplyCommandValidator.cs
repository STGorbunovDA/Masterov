using FluentValidation;

namespace Masterov.Domain.Masterov.Supply.DeleteSupply.Command;

public class DeleteSupplyCommandValidator : AbstractValidator<DeleteSupplyCommand>
{
    public DeleteSupplyCommandValidator()
    {
        RuleFor(q => q.SupplyId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplyId must not be an empty GUID.");}
}