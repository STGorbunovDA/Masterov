using FluentValidation;

namespace Masterov.Domain.Masterov.Order.AddOrder.Command;

public class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
{
    public AddOrderCommandValidator()
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
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("CustomerId must not be an empty GUID.");
    }
}
