using FluentValidation;

namespace Masterov.Domain.Masterov.Warehouse.GetSuppliesByWarehouseId.Query;

public class GetSuppliesByWarehouseIdQueryValidator : AbstractValidator<GetSuppliesByWarehouseIdQuery>
{
    public GetSuppliesByWarehouseIdQueryValidator()
    {
        RuleFor(q => q.WarehouseId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("WarehouseId must not be an empty GUID.");
    }
}