using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByStatus.Query;

public class GetPaymentsByStatusQueryValidator : AbstractValidator<GetPaymentsByStatusQuery>
{
    public GetPaymentsByStatusQueryValidator()
    {
        RuleFor(q => q.PaymentMethod).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithErrorCode("InvalidStatus")
            .WithMessage("Status must be a valid PaymentMethod value.")
            .Must(paymentMethod => paymentMethod != PaymentMethod.Unknown)
            .WithErrorCode("InvalidStatusValue")
            .WithMessage("Status cannot be 'Unknown'.");
    }
}