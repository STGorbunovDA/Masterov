using FluentValidation;

namespace Masterov.Domain.Masterov.UsedComponent.GetWarehouseByUsedComponentId.Query;

public class GetWarehouseByUsedComponentIdQueryValidator : AbstractValidator<GetWarehouseByUsedComponentIdQuery>
{
    public GetWarehouseByUsedComponentIdQueryValidator()
    {
        RuleFor(q => q.UsedComponentId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UsedComponentId must not be an empty GUID.");
    }
}