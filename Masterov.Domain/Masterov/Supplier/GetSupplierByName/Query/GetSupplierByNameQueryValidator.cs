using FluentValidation;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByName.Query;

public class GetSupplierByNameQueryValidator : AbstractValidator<GetSupplierByNameQuery>
{
    public GetSupplierByNameQueryValidator()
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