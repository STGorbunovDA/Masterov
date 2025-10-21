using FluentValidation;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByQuantity.Query;

public class GetUsedComponentsByQuantityQueryValidator : AbstractValidator<GetUsedComponentsByQuantityQuery>
{
    public GetUsedComponentsByQuantityQueryValidator()
    {
        RuleFor(q => q.Quantity).Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The quantity cannot be negative.");
    }
}