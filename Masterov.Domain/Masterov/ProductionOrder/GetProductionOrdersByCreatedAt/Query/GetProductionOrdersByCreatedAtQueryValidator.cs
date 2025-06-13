using FluentValidation;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt.Query;

public class GetByDeceasedDescriptionDeceasedsValidator : AbstractValidator<GetProductionOrdersByCreatedAtQuery>
{
    public GetByDeceasedDescriptionDeceasedsValidator()
    {
        RuleFor(q => q.CreatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow) // Проверка, что дата не в будущем
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
    }
}