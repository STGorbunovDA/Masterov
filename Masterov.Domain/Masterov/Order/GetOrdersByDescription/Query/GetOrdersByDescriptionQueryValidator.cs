using FluentValidation;

namespace Masterov.Domain.Masterov.Order.GetOrdersByDescription.Query;

public class GetOrdersByDescriptionQueryValidator : AbstractValidator<GetOrdersByDescriptionQuery>
{
    public GetOrdersByDescriptionQueryValidator()
    {
        RuleFor(q => q.Description).Cascade(CascadeMode.Stop)
            .MaximumLength(200)
            .WithErrorCode("DescriptionTooLong")
            .WithMessage("Description must be less than 200 characters.");
    }
}