using FluentValidation;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByUpdatedAt.Query;

public class GetComponentTypesByUpdatedAtQueryValidator : AbstractValidator<GetComponentTypesByUpdatedAtQuery>
{
    public GetComponentTypesByUpdatedAtQueryValidator()
    {
        RuleFor(q => q.UpdatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("UpdatedAt date cannot be in the future.");
    }
}