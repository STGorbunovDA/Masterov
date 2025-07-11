using FluentValidation;
using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder.Query;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder.Query;

public class GetProductComponentByOrderIdQueryValidator : AbstractValidator<GetProductComponentByOrderIdQuery>
{
    public GetProductComponentByOrderIdQueryValidator()
    {
        RuleFor(q => q.OrderId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be an empty GUID.");
    }
}