using FluentValidation;
using Masterov.Domain.Masterov.Payment.GetPaymentById.Query;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByOrderId.Query;

public class GetPaymentsByOrderIdQueryValidator : AbstractValidator<GetPaymentsByOrderIdQuery>
{
    public GetPaymentsByOrderIdQueryValidator()
    {
        RuleFor(q => q.OrderId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be an empty GUID.");
    }
}