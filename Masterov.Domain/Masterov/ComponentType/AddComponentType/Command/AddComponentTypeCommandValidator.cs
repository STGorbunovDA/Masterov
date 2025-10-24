using FluentValidation;

namespace Masterov.Domain.Masterov.ComponentType.AddComponentType.Command;

public class AddComponentTypeCommandValidator : AbstractValidator<AddComponentTypeCommand>
{
    public AddComponentTypeCommandValidator()
    {
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The name should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
        RuleFor(c => c.Description).Cascade(CascadeMode.Stop)
            .MaximumLength(200)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the description should not be more than 200");
    }
}