using FluentValidation;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByCreatedAt.Query;

public class GetSuppliesByCreatedAtQueryValidator : AbstractValidator<GetSuppliesByCreatedAtQuery>
{
    public GetSuppliesByCreatedAtQueryValidator()
    {
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}