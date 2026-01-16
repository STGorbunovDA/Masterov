using FluentValidation;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypesByUpdatedAt.Query;

public class GetProductTypesByUpdatedAtQueryValidator : AbstractValidator<GetProductTypesByUpdatedAtQuery>
{
    public GetProductTypesByUpdatedAtQueryValidator()
    {
        RuleFor(q => q.UpdatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("UpdatedAt date cannot be in the future.");
    }
}