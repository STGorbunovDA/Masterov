using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductOrders.Query;

public class GetByDeceasedDescriptionDeceasedsValidator : AbstractValidator<GetFinishedProductOrdersQuery>
{
    public GetByDeceasedDescriptionDeceasedsValidator()
    {
        RuleFor(q => q.FinishedProductId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ProductId must not be an empty GUID.");
        
        RuleFor(q => q.CreatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow) // Проверка, что дата не в будущем
            .When(q => q.CreatedAt.HasValue) // Проверка выполняется только если дата указана
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
            
        RuleFor(q => q.CompletedAt) 
            .LessThanOrEqualTo(DateTime.UtcNow) // Дата завершения не может быть в будущем
            .When(q => q.CompletedAt.HasValue) // Проверка выполняется только если дата указана
            .WithErrorCode("InvalidDate")
            .WithMessage("CompletedAt date cannot be in the future.")
            .GreaterThanOrEqualTo(q => q.CreatedAt) // Дата завершения не может быть раньше даты создания
            .When(q => q.CompletedAt.HasValue && q.CreatedAt.HasValue) // Проверка только если обе даты указаны
            .WithErrorCode("InvalidDateRange")
            .WithMessage("CompletedAt date cannot be before CreatedAt date.");
            
        RuleFor(q => q.Description)
            .MaximumLength(200)
            .When(q => !string.IsNullOrEmpty(q.Description))
            .WithErrorCode("DescriptionTooLong")
            .WithMessage("Description must be less than 200 characters.");
            
        RuleFor(q => q.Status)
            .IsInEnum()
            .WithErrorCode("InvalidStatus")
            .WithMessage("Status must be a valid ProductionOrderStatus value.")
            .Must(status => status != ProductionOrderStatus.Unknown)
            .WithErrorCode("InvalidStatusValue")
            .WithMessage("Status cannot be 'Unknown'.");
        
    }
}