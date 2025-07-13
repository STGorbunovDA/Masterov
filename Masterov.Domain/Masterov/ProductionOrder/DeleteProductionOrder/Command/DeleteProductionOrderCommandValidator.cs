using FluentValidation;

namespace Masterov.Domain.Masterov.ProductionOrder.DeleteProductionOrder.Command;

public class DeleteProductionOrderCommandValidator : AbstractValidator<DeleteProductionOrderCommand>
{
    public DeleteProductionOrderCommandValidator()
    {
        RuleFor(q => q.ProductionOrderId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ProductionOrderId must not be an empty GUID.");}
}