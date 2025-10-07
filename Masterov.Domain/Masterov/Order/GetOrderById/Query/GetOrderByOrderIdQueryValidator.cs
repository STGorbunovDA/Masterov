using FluentValidation;

namespace Masterov.Domain.Masterov.Order.GetOrderById.Query;

public class GetOrderByOrderIdQueryValidator : AbstractValidator<GetOrderByOrderIdQuery>
{
    public GetOrderByOrderIdQueryValidator()
    {
        RuleFor(q => q.OrderId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be an empty GUID.");
    }
}