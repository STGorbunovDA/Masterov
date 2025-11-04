using FluentValidation;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById.Query;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypeByWarehouseId.Query;

public class GetComponentTypeByWarehouseIdQueryValidator : AbstractValidator<GetComponentTypeByWarehouseIdQuery>
{
    public GetComponentTypeByWarehouseIdQueryValidator()
    {
        RuleFor(q => q.WarehouseId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("WarehouseId must not be an empty GUID.");
    }
}