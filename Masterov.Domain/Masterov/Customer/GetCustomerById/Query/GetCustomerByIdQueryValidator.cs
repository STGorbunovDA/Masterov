using FluentValidation;

namespace Masterov.Domain.Masterov.Customer.GetCustomerById.Query;

public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdQueryValidator()
    {
        RuleFor(q => q.CustomerId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("CustomerId must not be an empty GUID.");
    }
}