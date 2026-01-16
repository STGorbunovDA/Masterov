using FluentValidation;

namespace Masterov.Domain.Masterov.ProductType.GetProductTypesByCreatedAt.Query;

public class GetProductTypesByCreatedAtQueryValidator : AbstractValidator<GetProductTypesByCreatedAtQuery>
{
    public GetProductTypesByCreatedAtQueryValidator()
    {
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}