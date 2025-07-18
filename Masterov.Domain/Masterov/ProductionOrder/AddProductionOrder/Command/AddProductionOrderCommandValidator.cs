using FluentValidation;

namespace Masterov.Domain.Masterov.ProductionOrder.AddProductionOrder.Command;

public class AddProductionOrderCommandValidator : AbstractValidator<AddProductionOrderCommand>
{
    public AddProductionOrderCommandValidator()
    {
        RuleFor(q => q.FinishedProductId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("FinishedProductId must not be an empty GUID.");

        RuleFor(q => q.Description).Cascade(CascadeMode.Stop)
            .MaximumLength(200)
            .WithErrorCode("DescriptionTooLong")
            .WithMessage("Description must be less than 200 characters.");

        RuleFor(q => q.CustomerId).Cascade(CascadeMode.Stop)
            .Must(id => !id.HasValue || id.Value != Guid.Empty)
            .WithErrorCode("InvalidCustomerId")
            .WithMessage("CustomerId must not be an empty GUID if specified.");
        
        When(c => !c.CustomerId.HasValue || c.CustomerId.Value == Guid.Empty, () =>
        {
            RuleFor(c => c.CustomerName)
                .NotEmpty()
                .WithErrorCode("Empty")
                .WithMessage("The name should not be empty.")
                .MaximumLength(100)
                .WithErrorCode("TooLong")
                .WithMessage("The maximum length of the name should not be more than 100.");

            RuleFor(c => c.CustomerEmail)
                .Cascade(CascadeMode.Stop)
                .EmailAddress()
                .When(c => !string.IsNullOrEmpty(c.CustomerEmail))
                .WithErrorCode("InvalidEmail")
                .WithMessage("Invalid email address is specified.");

            RuleFor(c => c.CustomerPhone)
                .Cascade(CascadeMode.Stop)
                .Matches(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,20}$")
                .When(c => !string.IsNullOrEmpty(c.CustomerPhone))
                .WithErrorCode("InvalidPhone")
                .WithMessage("The phone number must contain between 7 and 20 digits.");

            RuleFor(c => c)
                .Must(c => !string.IsNullOrEmpty(c.CustomerEmail) || !string.IsNullOrEmpty(c.CustomerPhone))
                .WithErrorCode("ContactRequired")
                .WithMessage("You must provide an email or phone number for communication.");
        });
    }
}
