using FluentValidation;

namespace Masterov.Domain.Masterov.Order.GetUsedComponentsByOrderId.Query;

public class GetUsedComponentsByOrderIdQueryValidator : AbstractValidator<GetUsedComponentsByOrderIdQuery>
{
    public GetUsedComponentsByOrderIdQueryValidator()
    {
        RuleFor(q => q.OrderId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be an empty GUID.");
    }
}