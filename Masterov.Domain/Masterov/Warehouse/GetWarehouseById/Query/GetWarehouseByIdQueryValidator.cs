using FluentValidation;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehouseById.Query;

public class GetWarehouseByIdQueryValidator : AbstractValidator<GetWarehouseByIdQuery>
{
    public GetWarehouseByIdQueryValidator()
    {
        RuleFor(q => q.WarehouseId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("WarehouseById must not be an empty GUID.");
    }
}