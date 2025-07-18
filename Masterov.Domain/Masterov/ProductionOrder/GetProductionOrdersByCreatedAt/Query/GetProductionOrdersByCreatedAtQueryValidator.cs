using FluentValidation;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt.Query;

public class GetProductionOrdersByCreatedAtQueryValidator : AbstractValidator<GetProductionOrdersByCreatedAtQuery>
{
    public GetProductionOrdersByCreatedAtQueryValidator()
    {
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}