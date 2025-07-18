using FluentValidation;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCompletedAt.Query;

public class GetProductionOrdersByCompletedAtQueryValidator : AbstractValidator<GetProductionOrdersByCompletedAtQuery>
{
    public GetProductionOrdersByCompletedAtQueryValidator()
    {
        RuleFor(q => q.CompletedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithErrorCode("InvalidDate")
            .WithMessage("CompletedAt date cannot be in the future.");
    }
}