using FluentValidation;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByQuantity.Query;

public class GetSuppliesByQuantityValidator : AbstractValidator<GetSuppliesByQuantityQuery>
{
    public GetSuppliesByQuantityValidator()
    {
        RuleFor(q => q.Quantity).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The quantity cannot be negative.");
    }
}