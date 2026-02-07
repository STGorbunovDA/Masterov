using FluentValidation;
using Masterov.Domain.Masterov.ComponentType.AddComponentType.Command;

namespace Masterov.Domain.Masterov.ProductType.AddProductType.Command;

public class AddProductTypeCommandValidator : AbstractValidator<AddProductTypeCommand>
{
    public AddProductTypeCommandValidator()
    {
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The name should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
    }
}