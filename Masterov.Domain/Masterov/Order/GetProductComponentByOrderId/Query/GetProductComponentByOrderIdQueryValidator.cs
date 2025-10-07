using FluentValidation;

namespace Masterov.Domain.Masterov.Order.GetProductComponentByOrderId.Query;

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