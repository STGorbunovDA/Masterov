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
        
        RuleFor(q => q.PaymentMethod).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithErrorCode("InvalidStatus")
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
        
        RuleFor(c => c.NameCustomer).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The nameCustomer should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
        When(c => !string.IsNullOrEmpty(c.EmailCustomer), () =>
        {
            RuleFor(c => c.EmailCustomer)
                .EmailAddress()
                .WithErrorCode("InvalidEmailCustomer")
                .WithMessage("Invalid email address is specified.");
        });

        // Валидация телефона (если указан)
        When(c => !string.IsNullOrEmpty(c.PhoneCustomer), () =>
        {
            RuleFor(c => c.PhoneCustomer)
                .Matches(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$")
                .WithErrorCode("InvalidPhone")
                .WithMessage("The phone number must contain from 7 to 20 digits.");
        });

        // Обязательное условие: email ИЛИ телефон должны быть указаны
        RuleFor(c => c)
            .Must(c => !string.IsNullOrEmpty(c.EmailCustomer) || !string.IsNullOrEmpty(c.PhoneCustomer))
            .WithErrorCode("ContactRequired")
            .WithMessage("An email address or phone number must be specified for communication.");
        
    }
}