using FluentValidation;
namespace Masterov.Domain.Masterov.Supply.AddSupply.Command;

public class AddSupplyCommandValidator : AbstractValidator<AddSupplyCommand>
{
    public AddSupplyCommandValidator()
    {
        RuleFor(q => q.SupplierId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplierId must not be an empty GUID.");
        
        RuleFor(q => q.ComponentTypeId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ComponentTypeId must not be an empty GUID.");
        
        RuleFor(q => q.WarehouseId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("WarehouseId must not be an empty GUID.");
        
        RuleFor(q => q.Quantity).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The quantity cannot be negative.");
        
        RuleFor(q => q.Price).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The PriceSupply cannot be negative.");
    }
}