using FluentValidation;

namespace Masterov.Domain.Masterov.Supply.GetWarehouseBySupplyId.Query;

public class GetWarehouseBySupplyIdQueryValidator : AbstractValidator<GetWarehouseBySupplyIdQuery>
{
    public GetWarehouseBySupplyIdQueryValidator()
    {
        RuleFor(q => q.SupplyId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplyId must not be an empty GUID.");
    }
}