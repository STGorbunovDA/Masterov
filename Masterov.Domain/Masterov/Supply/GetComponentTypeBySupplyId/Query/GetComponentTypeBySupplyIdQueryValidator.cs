using FluentValidation;

namespace Masterov.Domain.Masterov.Supply.GetComponentTypeBySupplyId.Query;

public class GetComponentTypeBySupplyIdQueryValidator : AbstractValidator<GetComponentTypeBySupplyIdQuery>
{
    public GetComponentTypeBySupplyIdQueryValidator()
    {
        RuleFor(q => q.SupplyId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplyId must not be an empty GUID.");
    }
}