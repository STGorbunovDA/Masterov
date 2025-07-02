using FluentValidation;

namespace Masterov.Domain.Masterov.Payment.GetPaymentById.Query;

public class GetPaymentByIdQueryValidator : AbstractValidator<GetPaymentByIdQuery>
{
    public GetPaymentByIdQueryValidator()
    {
        RuleFor(q => q.PaymentId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("PaymentId must not be an empty GUID.");
    }
}