using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.Payment.AddPayment.Command;

public class AddPaymentCommandValidator : AbstractValidator<AddPaymentCommand>
{
    public AddPaymentCommandValidator()
    {
        RuleFor(q => q.OrderId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be an empty GUID.");
        
        RuleFor(q => q.CustomerId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("CustomerId must not be an empty GUID.");
        
        RuleFor(q => q.PaymentMethod).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithErrorCode("InvalidPaymentMethod")
            .WithMessage("PaymentMethod must be a valid PaymentMethod value.")
            .Must(payMethod => payMethod != PaymentMethod.Unknown)
            .WithErrorCode("InvalidPaymentMethodValue")
            .WithMessage("PaymentMethod cannot be 'Unknown'.");
        
        RuleFor(c => c.Amount)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithErrorCode("InvalidAmount")
            .WithMessage("Amount must be greater than 0.")
            .PrecisionScale(18, 2, ignoreTrailingZeros: true)
            .WithErrorCode("InvalidPrecision")
            .WithMessage("Amount must have no more than 2 decimal places.");
        
        
        
    }
}