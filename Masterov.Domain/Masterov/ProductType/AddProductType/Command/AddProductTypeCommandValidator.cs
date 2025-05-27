using FluentValidation;

namespace Masterov.Domain.Masterov.ProductType.AddProductType.Command;

public class AddProductTypeCommandValidator : AbstractValidator<AddProductTypeCommand>
{
    public AddProductTypeCommandValidator()
    {
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The name should not be empty.")
            .MaximumLength(50)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 50");
        
        RuleFor(c => c.Description).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The description should not be empty.")
            .MaximumLength(200)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the description should not be more than 200");
    }
}