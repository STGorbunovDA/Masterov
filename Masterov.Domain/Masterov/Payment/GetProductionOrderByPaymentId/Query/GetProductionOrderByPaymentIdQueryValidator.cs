using FluentValidation;

namespace Masterov.Domain.Masterov.Payment.GetProductionOrderByPaymentId.Query;

public class GetProductionOrderByPaymentIdQueryValidator : AbstractValidator<GetProductionOrderByPaymentIdQuery>
{
    public GetProductionOrderByPaymentIdQueryValidator()
    {
        RuleFor(q => q.PaymentId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("PaymentId must not be an empty GUID.");
    }
}