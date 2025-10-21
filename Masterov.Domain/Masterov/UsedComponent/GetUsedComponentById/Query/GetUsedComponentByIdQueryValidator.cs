using FluentValidation;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById.Query;

public class GetUsedComponentByIdQueryValidator : AbstractValidator<GetUsedComponentByIdQuery>
{
    public GetUsedComponentByIdQueryValidator()
    {
        RuleFor(q => q.UsedComponentId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UsedComponentId must not be an empty GUID.");
    }
}