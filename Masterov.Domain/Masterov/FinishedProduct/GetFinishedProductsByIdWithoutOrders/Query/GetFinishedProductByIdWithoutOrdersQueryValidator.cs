using FluentValidation;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByIdWithoutOrders.Query;

public class GetFinishedProductByIdWithoutOrdersQueryValidator : AbstractValidator<GetFinishedProductByIdWithoutOrdersQuery>
{
    public GetFinishedProductByIdWithoutOrdersQueryValidator()
    {
        RuleFor(q => q.FinishedProductId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("FinishedProductId must not be an empty GUID.");
    }
}