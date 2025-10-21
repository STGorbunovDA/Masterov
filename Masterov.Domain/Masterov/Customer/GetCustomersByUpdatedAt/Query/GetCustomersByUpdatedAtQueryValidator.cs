using FluentValidation;

namespace Masterov.Domain.Masterov.Customer.GetCustomersByUpdatedAt.Query;

public class GetCustomersByUpdatedAtQueryValidator : AbstractValidator<GetCustomersByUpdatedAtQuery>
{
    public GetCustomersByUpdatedAtQueryValidator()
    {
        RuleFor(q => q.UpdatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("UpdatedAt date cannot be in the future.");
    }
}