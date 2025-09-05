using FluentValidation;

namespace Masterov.Domain.Masterov.Supply.GetSupplierBySupplyId.Query;

public class GetSupplierBySupplyIdQueryValidator : AbstractValidator<GetSupplierBySupplyIdQuery>
{
    public GetSupplierBySupplyIdQueryValidator()
    {
        RuleFor(q => q.SupplyId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplyId must not be an empty GUID.");
    }
}