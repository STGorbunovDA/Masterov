using FluentValidation;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAtWithoutOrders.Query;

public class GetFinishedProductsByUpdatedAtWithoutOrdersQueryValidator : AbstractValidator<GetFinishedProductsByUpdatedAtWithoutOrdersQuery>
{
    public GetFinishedProductsByUpdatedAtWithoutOrdersQueryValidator()
    {
        RuleFor(q => q.UpdatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("UpdatedAt date cannot be in the future.");
    }
}