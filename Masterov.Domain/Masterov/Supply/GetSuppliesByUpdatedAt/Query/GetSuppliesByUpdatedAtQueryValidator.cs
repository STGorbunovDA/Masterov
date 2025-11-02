using FluentValidation;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByUpdatedAt.Query;

public class GetSuppliesByUpdatedAtQueryValidator : AbstractValidator<GetSuppliesByUpdatedAtQuery>
{
    public GetSuppliesByUpdatedAtQueryValidator()
    {
        RuleFor(q => q.UpdatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("UpdatedAt date cannot be in the future.");
    }
}