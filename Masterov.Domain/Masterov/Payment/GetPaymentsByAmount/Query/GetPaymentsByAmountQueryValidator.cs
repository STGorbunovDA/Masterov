using FluentValidation;

namespace Masterov.Domain.Masterov.Payment.GetPaymentsByAmount.Query;

public class GetPaymentsByAmountQueryValidator : AbstractValidator<GetPaymentsByAmountQuery>
{
    public GetPaymentsByAmountQueryValidator()
    {
        RuleFor(q => q.Amount).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The amount cannot be negative.");
    }
}