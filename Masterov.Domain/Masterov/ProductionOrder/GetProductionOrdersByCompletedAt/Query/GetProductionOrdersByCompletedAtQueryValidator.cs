using FluentValidation;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCompletedAt.Query;

public class GetByDeceasedDescriptionDeceasedsValidator : AbstractValidator<GetProductionOrdersByCompletedAtQuery>
{
    public GetByDeceasedDescriptionDeceasedsValidator()
    {
        RuleFor(q => q.CompletedAt)
            .LessThanOrEqualTo(DateTime.UtcNow) // Проверка, что дата не в будущем
            .WithErrorCode("InvalidDate")
            .WithMessage("CompletedAt date cannot be in the future.");
    }
}