using FluentValidation;
using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId.Query;

namespace Masterov.Domain.Masterov.Supplier.GetNewSuppliesBySupplierId.Query;

public class GetNewSuppliesBySupplierIdQueryValidator : AbstractValidator<GetNewSuppliesBySupplierIdQuery>
{
    public GetNewSuppliesBySupplierIdQueryValidator()
    {
        RuleFor(q => q.SupplierId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplierId must not be an empty GUID.");
    }
}