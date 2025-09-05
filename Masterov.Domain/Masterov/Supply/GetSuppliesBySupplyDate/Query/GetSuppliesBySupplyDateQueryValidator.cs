using FluentValidation;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesBySupplyDate.Query;

public class GetSuppliesBySupplyDateQueryValidator : AbstractValidator<GetSuppliesBySupplyDateQuery>
{
    public GetSuppliesBySupplyDateQueryValidator()
    {
        RuleFor(q => q.SupplyDate).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithErrorCode("InvalidDate")
            .WithMessage("SupplyDate date cannot be in the future.");
    }
}