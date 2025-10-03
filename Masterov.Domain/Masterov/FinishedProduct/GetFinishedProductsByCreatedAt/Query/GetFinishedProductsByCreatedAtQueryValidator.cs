using FluentValidation;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAt.Query;

public class GetFinishedProductsByCreatedAtQueryValidator : AbstractValidator<GetFinishedProductsByCreatedAtQuery>
{
    public GetFinishedProductsByCreatedAtQueryValidator()
    {
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}