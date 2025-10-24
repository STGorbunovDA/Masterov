using FluentValidation;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypeById.Query;

public class GetComponentTypeByIdQueryValidator : AbstractValidator<GetComponentTypeByIdQuery>
{
    public GetComponentTypeByIdQueryValidator()
    {
        RuleFor(q => q.ComponentTypeId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ComponentTypeId must not be an empty GUID.");
    }
}