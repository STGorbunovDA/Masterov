using FluentValidation;

namespace Masterov.Domain.Masterov.UsedComponent.GetComponentTypeByUsedComponentId.Query;

public class GetComponentTypeByUsedComponentIdQueryValidator : AbstractValidator<GetComponentTypeByUsedComponentIdQuery>
{
    public GetComponentTypeByUsedComponentIdQueryValidator()
    {
        RuleFor(q => q.UsedComponentId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UsedComponentId must not be an empty GUID.");
    }
}