using FluentValidation;
using Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByNameWithoutOrders.Query;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByTypeWithoutOrders.Query;

public class GetFinishedProductsByTypeWithoutOrdersQueryValidator : AbstractValidator<GetFinishedProductsByTypeWithoutOrdersQuery>
{
    public GetFinishedProductsByTypeWithoutOrdersQueryValidator()
    {
        RuleFor(c => c.FinishedProductType).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The finishedProductType should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the finishedProductType should not be more than 100");
    }
}