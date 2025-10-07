using FluentValidation;

namespace Masterov.Domain.Masterov.Payment.GetOrderByPaymentId.Query;

public class GetOrderByPaymentIdQueryValidator : AbstractValidator<GetOrderByPaymentIdQuery>
{
    public GetOrderByPaymentIdQueryValidator()
    {
        RuleFor(q => q.PaymentId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("PaymentId must not be an empty GUID.");
    }
}