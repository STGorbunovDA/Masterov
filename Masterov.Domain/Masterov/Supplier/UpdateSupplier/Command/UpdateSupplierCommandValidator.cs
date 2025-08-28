using FluentValidation;
using Masterov.Domain.Masterov.Customer.UpdateCustomer.Command;

namespace Masterov.Domain.Masterov.Supplier.UpdateSupplier.Command;

public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
{
    public UpdateSupplierCommandValidator()
    {
        RuleFor(q => q.SupplierId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplierId must not be an empty GUID.");
        
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
                .WithMessage("The address cannot be empty")
                .MaximumLength(200)
                .WithMessage("The address cannot be longer than 200 characters.");
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
            .WithMessage("You must provide an email or phone number for communication.");
    }
}