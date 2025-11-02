using FluentValidation;

namespace Masterov.Domain.Masterov.Supply.GetSupplyById.Query;

public class GetSupplyByIdQueryValidator : AbstractValidator<GetSupplyByIdQuery>
{
    public GetSupplyByIdQueryValidator()
    {
        RuleFor(q => q.SupplyId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplyId must not be an empty GUID.");
    }
}