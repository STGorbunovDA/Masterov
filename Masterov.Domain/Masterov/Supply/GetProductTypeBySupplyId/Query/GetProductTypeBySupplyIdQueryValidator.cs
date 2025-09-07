using FluentValidation;

namespace Masterov.Domain.Masterov.Supply.GetProductTypeBySupplyId.Query;

public class GetProductTypeBySupplyIdQueryValidator : AbstractValidator<GetProductTypeBySupplyIdQuery>
{
    public GetProductTypeBySupplyIdQueryValidator()
    {
        RuleFor(q => q.SupplyId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplyId must not be an empty GUID.");
    }
}