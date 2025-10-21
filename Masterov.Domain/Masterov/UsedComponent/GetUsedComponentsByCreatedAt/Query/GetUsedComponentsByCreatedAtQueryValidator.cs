using FluentValidation;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByCreatedAt.Query;

public class GetUsedComponentsByCreatedAtQueryValidator : AbstractValidator<GetUsedComponentsByCreatedAtQuery>
{
    public GetUsedComponentsByCreatedAtQueryValidator()
    {
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}