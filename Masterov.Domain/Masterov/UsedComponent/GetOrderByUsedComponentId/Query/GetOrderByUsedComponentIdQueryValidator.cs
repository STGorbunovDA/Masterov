using FluentValidation;
using Masterov.Domain.Masterov.UsedComponent.GetOrderByUsedComponentId.Query;

namespace Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId.Query;

public class GetOrderByUsedComponentIdQueryValidator : AbstractValidator<GetOrderByUsedComponentIdQuery>
{
    public GetOrderByUsedComponentIdQueryValidator()
    {
        RuleFor(q => q.UsedComponentId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UsedComponentId must not be an empty GUID.");
    }
}