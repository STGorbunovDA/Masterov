using FluentValidation;

namespace Masterov.Domain.Masterov.Customer.GetCustomerByEmail.Query;

public class GetCustomerByEmailQueryValidator : AbstractValidator<GetCustomerByEmailQuery>
{
    public GetCustomerByEmailQueryValidator()
    {
        RuleFor(c => c.Email)
            .EmailAddress()
            .WithErrorCode("InvalidEmailCustomer")
            .WithMessage("Invalid email address is specified.");
    }
}