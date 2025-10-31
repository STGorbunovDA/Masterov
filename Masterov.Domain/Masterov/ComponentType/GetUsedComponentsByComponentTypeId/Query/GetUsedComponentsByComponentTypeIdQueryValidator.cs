using FluentValidation;

namespace Masterov.Domain.Masterov.ComponentType.GetUsedComponentsByComponentTypeId.Query;

public class GetUsedComponentsByComponentTypeIdQueryValidator : AbstractValidator<GetUsedComponentsByComponentTypeIdQuery>
{
    public GetUsedComponentsByComponentTypeIdQueryValidator()
    {
        RuleFor(q => q.ComponentTypeId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ComponentTypeId must not be an empty GUID.");
    }
}