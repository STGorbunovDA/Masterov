using FluentValidation;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersBySurname.Query;

public class GetSuppliersBySurnameQueryValidator : AbstractValidator<GetSuppliersBySurnameQuery>
{
    public GetSuppliersBySurnameQueryValidator()
    {
        RuleFor(c => c.SupplierSurname).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The SupplierSurname should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the SupplierSurname should not be more than 100");
    }
}