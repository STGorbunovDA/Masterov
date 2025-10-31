using FluentValidation;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByCreatedAt.Query;

public class GetComponentTypesByCreatedAtQueryValidator : AbstractValidator<GetComponentTypesByCreatedAtQuery>
{
    public GetComponentTypesByCreatedAtQueryValidator()
    {
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}