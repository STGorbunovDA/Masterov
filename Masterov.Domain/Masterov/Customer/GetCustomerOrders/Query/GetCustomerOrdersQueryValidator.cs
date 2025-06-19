using FluentValidation;

namespace Masterov.Domain.Masterov.Customer.GetCustomerOrders.Query;

public class GetCustomerOrdersQueryValidator : AbstractValidator<GetCustomerOrdersQuery>
{
    public GetCustomerOrdersQueryValidator()
    {
        RuleFor(q => q.CustomerId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("CustomerId must not be an empty GUID.");
    }
}