using FluentValidation;

namespace Masterov.Domain.Masterov.ProductionOrder.GetCustomerByOrderId.Query;

public class GetCustomerByOrderIdQueryValidator : AbstractValidator<GetCustomerByOrderIdQuery>
{
    public GetCustomerByOrderIdQueryValidator()
    {
        RuleFor(q => q.OrderId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be an empty GUID.");
    }
}