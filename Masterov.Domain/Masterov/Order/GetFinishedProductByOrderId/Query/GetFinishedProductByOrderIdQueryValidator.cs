using FluentValidation;

namespace Masterov.Domain.Masterov.Order.GetFinishedProductByOrderId.Query;

public class GetFinishedProductByOrderIdQueryValidator : AbstractValidator<GetFinishedProductByOrderIdQuery>
{
    public GetFinishedProductByOrderIdQueryValidator()
    {
        RuleFor(q => q.OrderId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be an empty GUID.");
    }
}