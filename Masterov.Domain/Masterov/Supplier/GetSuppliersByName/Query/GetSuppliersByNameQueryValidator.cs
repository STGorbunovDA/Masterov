using FluentValidation;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByName.Query;

public class GetSuppliersByNameQueryValidator : AbstractValidator<GetSuppliersByNameQuery>
{
    public GetSuppliersByNameQueryValidator()
    {
        RuleFor(c => c.SupplierName).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The SupplierName should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the SupplierName should not be more than 100");
    }
}