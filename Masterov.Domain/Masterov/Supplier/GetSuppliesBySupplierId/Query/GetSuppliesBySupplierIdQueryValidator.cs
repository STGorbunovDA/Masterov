using FluentValidation;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliesBySupplierId.Query;

public class GetSuppliesBySupplierIdQueryValidator : AbstractValidator<GetSuppliesBySupplierIdIdQuery>
{
    public GetSuppliesBySupplierIdQueryValidator()
    {
        RuleFor(q => q.SupplierId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplierId must not be an empty GUID.");
    }
}