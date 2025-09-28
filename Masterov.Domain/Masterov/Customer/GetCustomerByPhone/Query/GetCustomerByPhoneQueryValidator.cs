using FluentValidation;

namespace Masterov.Domain.Masterov.Customer.GetCustomerByPhone.Query;

public class GetCustomerByPhoneQueryValidator : AbstractValidator<GetCustomerByPhoneQuery>
{
    public GetCustomerByPhoneQueryValidator()
    {
        RuleFor(c => c.Phone)
            .Matches(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$")
            .WithErrorCode("InvalidPhone")
            .WithMessage("The phone number must contain from 7 to 10 digits.");
    }
}