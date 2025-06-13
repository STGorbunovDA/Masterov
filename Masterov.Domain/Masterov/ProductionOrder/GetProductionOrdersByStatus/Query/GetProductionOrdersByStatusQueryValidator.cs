using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus.Query;

public class GetProductionOrdersByStatusQueryValidator : AbstractValidator<GetProductionOrdersByStatusQuery>
{
    public GetProductionOrdersByStatusQueryValidator()
    {
        RuleFor(q => q.Status)
            .IsInEnum()
            .WithErrorCode("InvalidStatus")
            .WithMessage("Status must be a valid ProductionOrderStatus value.")
            .Must(status => status != ProductionOrderStatus.Unknown)
            .WithErrorCode("InvalidStatusValue")
            .WithMessage("Status cannot be 'Unknown'.");
    }
}