using FluentValidation;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById.Query;

namespace Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder.Query;

public class GetFinishedProductAtOrderQueryValidator : AbstractValidator<GetFinishedProductAtOrderQuery>
{
    public GetFinishedProductAtOrderQueryValidator()
    {
        RuleFor(q => q.OrderId)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be an empty GUID.");
    }
}