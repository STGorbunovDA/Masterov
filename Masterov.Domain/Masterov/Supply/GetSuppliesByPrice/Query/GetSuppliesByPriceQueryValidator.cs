using FluentValidation;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByPrice.Query;

public class GetSuppliesByPriceQueryValidator : AbstractValidator<GetSuppliesByPriceQuery>
{
    public GetSuppliesByPriceQueryValidator()
    {
        RuleFor(q => q.Price).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The PriceSupply cannot be negative.");
    }
}