using FluentValidation;

namespace Masterov.Domain.Masterov.Warehouse.UpdatePriceWarehouseById.Command;

public class UpdatePriceWarehouseByIdCommandValidator : AbstractValidator<UpdatePriceWarehouseByIdCommand>
{
    public UpdatePriceWarehouseByIdCommandValidator()
    {
        RuleFor(q => q.WarehouseId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("WarehouseId must not be an empty GUID.");
        
        RuleFor(q => q.Price).Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithMessage("The PriceSupply must be greater than zero.");
    }
}