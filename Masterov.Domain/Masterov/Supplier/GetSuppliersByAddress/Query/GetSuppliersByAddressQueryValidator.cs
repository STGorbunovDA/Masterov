using FluentValidation;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByAddress.Query;

public class GetSuppliersByAddressQueryValidator : AbstractValidator<GetSuppliersByAddressQuery>
{
    public GetSuppliersByAddressQueryValidator()
    {
        RuleFor(c => c.Address)
            .NotEmpty()
            .WithMessage("The address cannot be empty")
            .MaximumLength(200)
            .WithMessage("The address cannot be longer than 200 characters.");
    }
}