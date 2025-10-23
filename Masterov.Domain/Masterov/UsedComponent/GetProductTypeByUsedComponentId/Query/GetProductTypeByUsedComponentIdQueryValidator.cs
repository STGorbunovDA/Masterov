using FluentValidation;

namespace Masterov.Domain.Masterov.UsedComponent.GetProductTypeByUsedComponentId.Query;

public class GetProductTypeByUsedComponentIdQueryValidator : AbstractValidator<GetProductTypeByUsedComponentIdQuery>
{
    public GetProductTypeByUsedComponentIdQueryValidator()
    {
        RuleFor(q => q.UsedComponentId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UsedComponentId must not be an empty GUID.");
    }
}