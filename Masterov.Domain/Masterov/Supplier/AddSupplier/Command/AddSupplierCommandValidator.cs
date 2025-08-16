using FluentValidation;

namespace Masterov.Domain.Masterov.Supplier.AddSupplier.Command;

public class AddSupplierCommandValidator : AbstractValidator<AddSupplierCommand>
{
    public AddSupplierCommandValidator()
    {
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The name should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
        When(c => !string.IsNullOrEmpty(c.Address), () =>
        {
            RuleFor(c => c.Address)
                .NotEmpty()
                .WithErrorCode("Empty")
                .WithMessage("The name should not be empty.")
                .WithErrorCode("InvalidAddress")
                .MaximumLength(200)
                .WithErrorCode("TooLong")
                .WithMessage("Invalid Address is specified.");
        });

        // Валидация телефона (если указан)
        When(c => !string.IsNullOrEmpty(c.Phone), () =>
        {
            RuleFor(c => c.Phone)
                .Matches(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$")
                .WithErrorCode("InvalidPhone")
                .WithMessage("The phone number must contain between 7 and 20 digits.");
        });

        // Обязательное условие: email ИЛИ телефон должны быть указаны
        RuleFor(c => c)
            .Must(c => !string.IsNullOrEmpty(c.Address) || !string.IsNullOrEmpty(c.Phone))
            .WithErrorCode("ContactRequired")
            .WithMessage("You must provide an address or phone number for communication.");
        
    }
}