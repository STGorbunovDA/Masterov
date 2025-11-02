using FluentValidation;
using Masterov.Domain.Extension;

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
        
        RuleFor(c => c.Surname).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The name should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
        RuleFor(c => c.Address).Cascade(CascadeMode.Stop)
            .MaximumLength(200)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
        // Валидация даты создания (CreatedAt)
        RuleFor(c => c.CreatedAt)
            .Must(DomainExtension.BeValidPastOrPresentDate)
            .When(c => c.CreatedAt.HasValue)
            .WithErrorCode("InvalidCreatedAt")
            .WithMessage("Creation date cannot be in the future.");
        
        // Валидация телефона (если указан)
        When(c => !string.IsNullOrEmpty(c.Phone), () =>
        {
            RuleFor(c => c.Phone)
                .Matches(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$")
                .WithErrorCode("InvalidPhone")
                .WithMessage("The phone number must contain between 7 and 10 digits.");
        });
        
        When(c => !string.IsNullOrEmpty(c.Email), () =>
        {
            RuleFor(c => c.Email)
                .EmailAddress()
                .WithErrorCode("InvalidEmail")
                .WithMessage("Invalid email address is specified.");
        });
        
        // Обязательное условие: email ИЛИ телефон должны быть указаны
        RuleFor(c => c)
            .Must(c => !string.IsNullOrEmpty(c.Email) || !string.IsNullOrEmpty(c.Phone))
            .WithErrorCode("ContactRequired")
            .WithMessage("You must provide an email or phone number for communication.");
    }
}