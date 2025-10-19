using FluentValidation;

namespace Masterov.Domain.Masterov.Order.GetProductComponentByOrderId.Query;

public class GetComponentsByOrderIdQueryValidator : AbstractValidator<GetComponentsByOrderIdQuery>
{
    public GetComponentsByOrderIdQueryValidator()
    {
        RuleFor(q => q.OrderId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be an empty GUID.");
    }
}