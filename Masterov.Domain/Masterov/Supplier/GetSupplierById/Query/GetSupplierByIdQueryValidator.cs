using FluentValidation;
using Masterov.Domain.Masterov.Customer.GetCustomerById.Query;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierById.Query;

public class GetSupplierByIdQueryValidator : AbstractValidator<GetSupplierByIdQuery>
{
    public GetSupplierByIdQueryValidator()
    {
        RuleFor(q => q.SupplierId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplierId must not be an empty GUID.");
    }
}