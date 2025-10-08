using FluentValidation;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByNameWithoutOrders.Query;

public class GetFinishedProductByNameWithoutOrdersValidator : AbstractValidator<GetFinishedProductByNameWithoutOrdersQuery>
{
    public GetFinishedProductByNameWithoutOrdersValidator()
    {
        RuleFor(c => c.FinishedProductName).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The finishedProductName should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the finishedProductName should not be more than 100");
    }
}