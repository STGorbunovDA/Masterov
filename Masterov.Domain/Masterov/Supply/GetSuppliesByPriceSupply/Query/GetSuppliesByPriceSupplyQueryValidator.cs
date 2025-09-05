using FluentValidation;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByPriceSupply.Query;

public class GetSuppliesByPriceSupplyQueryValidator : AbstractValidator<GetSuppliesByAmountPriceSupply>
{
    public GetSuppliesByPriceSupplyQueryValidator()
    {
        RuleFor(q => q.PriceSupply).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The amount cannot be negative.");
    }
}