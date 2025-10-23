using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.UsedComponent.UpdateUsedComponent.Command;

public class UpdateUsedComponentCommandValidator : AbstractValidator<UpdateUsedComponentCommand>
{
    public UpdateUsedComponentCommandValidator()
    {
        RuleFor(q => q.UsedComponentId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("UsedComponentId must not be an empty GUID.");
        
        RuleFor(q => q.OrderId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be an empty GUID.");
        
        RuleFor(q => q.ProductTypeId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ProductTypeId must not be an empty GUID.");
        
        RuleFor(q => q.WarehouseId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("WarehouseId must not be an empty GUID.");
        
        RuleFor(q => q.Quantity).Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
        
        RuleFor(c => c.CreatedAt)
            .Must(DomainExtension.BeValidPastOrPresentDate)
            .When(c => c.CreatedAt.HasValue)
            .WithErrorCode("InvalidCreatedAt")
            .WithMessage("Creation date cannot be in the future.");
    }
}