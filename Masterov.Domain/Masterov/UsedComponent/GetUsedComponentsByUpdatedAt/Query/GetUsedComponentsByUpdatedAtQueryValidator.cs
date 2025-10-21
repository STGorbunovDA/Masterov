using FluentValidation;
using Masterov.Domain.Masterov.Customer.GetCustomersByUpdatedAt.Query;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByUpdatedAt.Query;

public class GetUsedComponentsByUpdatedAtQueryValidator : AbstractValidator<GetUsedComponentsByUpdatedAtQuery>
{
    public GetUsedComponentsByUpdatedAtQueryValidator()
    {
        RuleFor(q => q.UpdatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("UpdatedAt date cannot be in the future.");
    }
}