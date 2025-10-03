using FluentValidation;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAt.Query;

public class GetFinishedProductsByUpdatedAtQueryValidator : AbstractValidator<GetFinishedProductsByUpdatedAtQuery>
{
    public GetFinishedProductsByUpdatedAtQueryValidator()
    {
        RuleFor(q => q.UpdatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("UpdatedAt date cannot be in the future.");
    }
}