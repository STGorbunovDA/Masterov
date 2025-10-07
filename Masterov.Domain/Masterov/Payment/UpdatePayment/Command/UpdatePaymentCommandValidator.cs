using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.Payment.UpdatePayment.Command;

public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
{
    public UpdatePaymentCommandValidator()
    {
        RuleFor(q => q.PaymentId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("PaymentId must not be an empty GUID.");
        
        RuleFor(q => q.OrderId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be an empty GUID.");
        
        RuleFor(q => q.CustomerId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("CustomerId must not be an empty GUID.");
        
        RuleFor(q => q.MethodPayment).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithErrorCode("InvalidMethodPayment")
            .WithMessage("Status must be a valid PaymentMethod value.")
            .Must(paymentMethod => paymentMethod != PaymentMethod.Unknown)
            .WithErrorCode("InvalidMethodPaymentValue")
            .WithMessage("MethodPayment cannot be 'Unknown'.");
        
        RuleFor(c => c.Amount)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithErrorCode("InvalidAmount")
            .WithMessage("Amount must be greater than 0.")
            .PrecisionScale(18, 2, ignoreTrailingZeros: true)
            .WithErrorCode("InvalidPrecision")
            .WithMessage("Amount must have no more than 2 decimal places.");
        
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
        
    }
}