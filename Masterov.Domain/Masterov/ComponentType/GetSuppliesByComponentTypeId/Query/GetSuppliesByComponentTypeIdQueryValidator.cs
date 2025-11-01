using FluentValidation;

namespace Masterov.Domain.Masterov.ComponentType.GetSuppliesByComponentTypeId.Query;

public class GetSuppliesByComponentTypeIdQueryValidator : AbstractValidator<GetSuppliesByComponentTypeIdQuery>
{
    public GetSuppliesByComponentTypeIdQueryValidator()
    {
        RuleFor(q => q.ComponentTypeId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ComponentTypeId must not be an empty GUID.");
    }
}