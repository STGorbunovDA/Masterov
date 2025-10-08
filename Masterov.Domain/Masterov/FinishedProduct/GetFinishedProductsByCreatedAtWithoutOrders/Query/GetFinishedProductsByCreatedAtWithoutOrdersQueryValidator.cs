using FluentValidation;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAtWithoutOrders.Query;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAt.Query;

public class GetFinishedProductsByCreatedAtWithoutOrdersQueryValidator : AbstractValidator<GetFinishedProductsByCreatedAtWithoutOrdersQuery>
{
    public GetFinishedProductsByCreatedAtWithoutOrdersQueryValidator()
    {
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}