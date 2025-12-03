using FluentValidation;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByType.Query;

public class GetFinishedProductsByTypeQueryValidator : AbstractValidator<GetFinishedProductsByTypeQuery>
{
    public GetFinishedProductsByTypeQueryValidator()
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