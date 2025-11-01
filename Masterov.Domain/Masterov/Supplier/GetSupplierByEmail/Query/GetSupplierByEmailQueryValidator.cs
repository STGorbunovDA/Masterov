using FluentValidation;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByEmail.Query;

public class GetSupplierByEmailQueryValidator : AbstractValidator<GetSupplierByEmailQuery>
{
    public GetSupplierByEmailQueryValidator()
    {
        RuleFor(c => c.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithErrorCode("Required")
                .WithMessage("Email is required.")
            .EmailAddress()
                .WithErrorCode("InvalidEmailCustomer")
                .WithMessage("Invalid email address is specified.")
            .MaximumLength(100)
                .WithErrorCode("TooLong")
                .WithMessage("The maximum length of the email should not be more than 100");
    }
}