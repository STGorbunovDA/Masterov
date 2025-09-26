using FluentValidation;
using Masterov.Domain.Masterov.Supply.UpdateSupply.Command;

namespace Masterov.Domain.Masterov.Warehouse.UpdateWarehouse.Command;

public class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
{
    public UpdateWarehouseCommandValidator()
    {
        RuleFor(q => q.WarehouseId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplyId must not be an empty GUID.");
        
        RuleFor(q => q.ProductTypeId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplierId must not be an empty GUID.");
        
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The name should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
        RuleFor(q => q.Quantity).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The quantity cannot be negative.");
        
        RuleFor(q => q.Price).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The PriceSupply cannot be negative.");
    }
}