using FluentValidation;

namespace Masterov.Domain.Masterov.UsedComponent.AddUsedComponent.Command;

public class AddUsedComponentCommandValidator : AbstractValidator<AddUsedComponentCommand>
{
    public AddUsedComponentCommandValidator()
    {
        RuleFor(q => q.OrderId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be an empty GUID.");
        
        RuleFor(q => q.ComponentTypeId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ComponentTypeId must not be an empty GUID.");
        
        RuleFor(q => q.WarehouseId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("WarehouseId must not be an empty GUID.");
        
        RuleFor(q => q.Quantity).Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
        
    }
}