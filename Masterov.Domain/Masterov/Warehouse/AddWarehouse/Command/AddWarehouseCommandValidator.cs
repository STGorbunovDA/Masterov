using FluentValidation;

namespace Masterov.Domain.Masterov.Warehouse.AddWarehouse.Command;

public class AddWarehouseCommandValidator : AbstractValidator<AddWarehouseCommand>
{
    public AddWarehouseCommandValidator()
    {
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The Name should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the Name should not be more than 100");
        
        RuleFor(q => q.ComponentTypeId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ComponentTypeId must not be an empty GUID.");
        
        
    }
}