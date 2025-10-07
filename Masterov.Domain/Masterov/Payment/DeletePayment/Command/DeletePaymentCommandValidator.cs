using FluentValidation;

namespace Masterov.Domain.Masterov.Payment.DeletePayment.Command;

public class DeletePaymentCommandValidator : AbstractValidator<DeletePaymentCommand>
{
    public DeletePaymentCommandValidator()
    {
        RuleFor(q => q.PaymentId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("PaymentId must not be an empty GUID.");}
}