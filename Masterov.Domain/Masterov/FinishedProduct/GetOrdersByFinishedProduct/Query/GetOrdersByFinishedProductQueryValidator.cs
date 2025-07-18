using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.FinishedProduct.GetOrdersByFinishedProduct.Query;

public class GetOrdersByFinishedProductQueryValidator : AbstractValidator<GetOrdersByFinishedProductQuery>
{
    public GetOrdersByFinishedProductQueryValidator()
    {
        RuleFor(q => q.FinishedProductId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("FinishedProductId must not be an empty GUID.");
        
        RuleFor(q => q.CreatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .When(q => q.CreatedAt.HasValue)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
            
        RuleFor(q => q.CompletedAt).Cascade(CascadeMode.Stop) 
            .LessThanOrEqualTo(DateTime.UtcNow)
            .When(q => q.CompletedAt.HasValue)
            .WithErrorCode("InvalidDate")
            .WithMessage("CompletedAt date cannot be in the future.")
            .GreaterThanOrEqualTo(q => q.CreatedAt)
            .When(q => q.CompletedAt.HasValue && q.CreatedAt.HasValue)
            .WithErrorCode("InvalidDateRange")
            .WithMessage("CompletedAt date cannot be before CreatedAt date.");
            
        RuleFor(q => q.Description).Cascade(CascadeMode.Stop)
            .MaximumLength(200)
            .When(q => !string.IsNullOrEmpty(q.Description))
            .WithErrorCode("DescriptionTooLong")
            .WithMessage("Description must be less than 200 characters.");
            
        RuleFor(q => q.Status).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithErrorCode("InvalidStatus")
            .WithMessage("Status must be a valid ProductionOrderStatus value.")
            .Must(status => status != ProductionOrderStatus.Unknown)
            .WithErrorCode("InvalidStatusValue")
            .WithMessage("Status cannot be 'Unknown'.");
        
    }
}