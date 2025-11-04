using FluentValidation;

namespace Masterov.Domain.Masterov.Warehouse.DeleteWarehouse.Command;

public class DeleteWarehouseCommandValidator : AbstractValidator<DeleteWarehouseCommand>
{
    public DeleteWarehouseCommandValidator()
    {
        RuleFor(q => q.WarehouseId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("WarehouseId must not be an empty GUID.");}
}