using FluentValidation;

namespace Masterov.Domain.Masterov.Customer.GetCustomersByCreatedAt.Query;

public class GetCustomersByCreatedAtQueryValidator : AbstractValidator<GetCustomersByCreatedAtQuery>
{
    public GetCustomersByCreatedAtQueryValidator()
    {
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}