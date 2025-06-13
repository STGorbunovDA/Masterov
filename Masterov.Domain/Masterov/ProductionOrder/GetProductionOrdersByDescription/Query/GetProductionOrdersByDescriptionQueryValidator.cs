using FluentValidation;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByDescription.Query;

public class GetProductionOrdersByDescriptionQueryValidator : AbstractValidator<GetProductionOrdersByDescriptionQuery>
{
    public GetProductionOrdersByDescriptionQueryValidator()
    {
        RuleFor(q => q.Description)
            .MaximumLength(200)
            .WithErrorCode("DescriptionTooLong")
            .WithMessage("Description must be less than 200 characters.");
    }
}