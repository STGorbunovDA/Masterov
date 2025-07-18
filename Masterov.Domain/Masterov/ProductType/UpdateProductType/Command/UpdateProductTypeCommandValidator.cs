using FluentValidation;

namespace Masterov.Domain.Masterov.ProductType.UpdateProductType.Command;

public class UpdateProductTypeCommandValidator : AbstractValidator<UpdateProductTypeCommand>
{
    public UpdateProductTypeCommandValidator()
    {
        RuleFor(q => q.ProductTypeId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("productTypeId must not be an empty GUID.");
        
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