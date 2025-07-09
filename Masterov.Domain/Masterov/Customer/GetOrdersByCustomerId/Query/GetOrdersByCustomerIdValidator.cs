using FluentValidation;

namespace Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId.Query;

public class GetOrdersByCustomerIdValidator : AbstractValidator<GetOrdersByCustomerIdQuery>
{
    public GetOrdersByCustomerIdValidator()
    {
        RuleFor(q => q.CustomerId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("CustomerId must not be an empty GUID.");
    }
}