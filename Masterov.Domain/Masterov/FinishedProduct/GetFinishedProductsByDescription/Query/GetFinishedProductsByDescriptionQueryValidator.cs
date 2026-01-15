using FluentValidation;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByDescription.Query;

public class GetFinishedProductsByDescriptionQueryValidator : AbstractValidator<GetFinishedProductsByDescriptionQuery>
{
    public GetFinishedProductsByDescriptionQueryValidator()
    {
        RuleFor(q => q.Description).Cascade(CascadeMode.Stop)
            .MaximumLength(300)
            .WithErrorCode("DescriptionTooLong")
            .WithMessage("Description must be less than 300 characters.");
    }
}