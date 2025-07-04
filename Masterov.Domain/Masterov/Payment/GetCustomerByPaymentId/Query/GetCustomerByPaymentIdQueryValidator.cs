using FluentValidation;
using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder.Query;

namespace Masterov.Domain.Masterov.Payment.GetCustomerByPaymentId.Query;

public class GetCustomerByPaymentIdQueryValidator : AbstractValidator<GetCustomerByPaymentIdQuery>
{
    public GetCustomerByPaymentIdQueryValidator()
    {
        RuleFor(q => q.PaymentId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("PaymentId must not be an empty GUID.");
    }
}