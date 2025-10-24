using FluentValidation;

namespace Masterov.Domain.Masterov.Customer.AddCustomer.Command;

public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
{
    public AddCustomerCommandValidator()
    {
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The name should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
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
        
        When(c => c.UserId.HasValue, () =>
        {
            RuleFor(c => c.UserId)
                .Must(id => id != Guid.Empty)
                .WithErrorCode("InvalidUserId")
                .WithMessage("The specified UserId cannot be an empty GUID.");
        });
        
    }
}