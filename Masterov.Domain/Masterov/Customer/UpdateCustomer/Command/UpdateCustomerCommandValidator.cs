using FluentValidation;

namespace Masterov.Domain.Masterov.Customer.UpdateCustomer.Command;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(q => q.CustomerId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("CustomerId must not be an empty GUID.");
        
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The name should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
        When(c => !string.IsNullOrEmpty(c.Email), () =>
        {
            RuleFor(c => c.Email)
                .EmailAddress()
                .WithErrorCode("InvalidEmail")
                .WithMessage("Invalid email address is specified.");
        });

        // Валидация телефона (если указан)
        When(c => !string.IsNullOrEmpty(c.Phone), () =>
        {
            RuleFor(c => c.Phone)
                .Matches(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$")
                .WithErrorCode("InvalidPhone")
                .WithMessage("The phone number must contain between 7 and 20 digits.");
        });

        // Валидация даты создания (CreatedAt)
        RuleFor(c => c.CreatedAt)
            .Must(BeValidPastOrPresentDate)
            .When(c => c.CreatedAt.HasValue)
            .WithErrorCode("InvalidCreatedAt")
            .WithMessage("Creation date cannot be in the future.");

        
        // Обязательное условие: email ИЛИ телефон должны быть указаны
        RuleFor(c => c)
            .Must(c => !string.IsNullOrEmpty(c.Email) || !string.IsNullOrEmpty(c.Phone))
            .WithErrorCode("ContactRequired")
            .WithMessage("You must provide an email or phone number for communication.");
    }

    private bool BeValidPastOrPresentDate(DateTime? date)
    {
        return !date.HasValue || date.Value <= DateTime.Now;
    }
}