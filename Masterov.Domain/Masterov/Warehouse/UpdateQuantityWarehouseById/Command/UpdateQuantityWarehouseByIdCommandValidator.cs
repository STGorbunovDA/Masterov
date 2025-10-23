using FluentValidation;

namespace Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById.Command;

public class UpdateQuantityWarehouseByIdCommandValidator : AbstractValidator<UpdateQuantityWarehouseByIdCommand>
{
    public UpdateQuantityWarehouseByIdCommandValidator()
    {
        RuleFor(q => q.WarehouseId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplyId must not be an empty GUID.");
        
        RuleFor(q => q.Quantity).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The quantity cannot be negative.");
    }
}