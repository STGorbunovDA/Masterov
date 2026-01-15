using FluentValidation;
namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByDescriptionAtWithoutOrders.Query;

public class GetFinishedProductsByDescriptionQueryAtWithoutOrdersValidator : AbstractValidator<GetFinishedProductsByDescriptionAtWithoutOrdersQuery>
{
    public GetFinishedProductsByDescriptionQueryAtWithoutOrdersValidator()
    {
        RuleFor(q => q.Description).Cascade(CascadeMode.Stop)
            .MaximumLength(300)
            .WithErrorCode("DescriptionTooLong")
            .WithMessage("Description must be less than 300 characters.");
    }
}