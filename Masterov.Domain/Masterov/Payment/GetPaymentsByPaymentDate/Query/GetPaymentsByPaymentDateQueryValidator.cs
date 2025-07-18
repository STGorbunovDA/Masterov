using FluentValidation;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByPaymentDate.Query;

public class GetPaymentsByPaymentDateQueryValidator : AbstractValidator<GetPaymentsByPaymentDateQuery>
{
    public GetPaymentsByPaymentDateQueryValidator()
    {
        RuleFor(q => q.PaymentDate).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithErrorCode("InvalidDate")
            .WithMessage("PaymentDate date cannot be in the future.");
    }
}