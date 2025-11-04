using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.Supply.UpdateSupply.Command;

public class UpdateSupplyCommandValidator : AbstractValidator<UpdateSupplyCommand>
{
    public UpdateSupplyCommandValidator()
    {
        RuleFor(q => q.SupplyId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplyId must not be an empty GUID.");
        
        RuleFor(q => q.SupplierId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("SupplierId must not be an empty GUID.");
        
        RuleFor(q => q.ComponentTypeId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ComponentTypeId must not be an empty GUID.");
        
        RuleFor(q => q.WarehouseId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("WarehouseId must not be an empty GUID.");
        
        RuleFor(q => q.Quantity).Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithMessage("The quantity must be greater than zero.");
        
        RuleFor(q => q.Price).Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithMessage("The PriceSupply must be greater than zero.");
        
        // Валидация даты создания (CreatedAt)
        RuleFor(c => c.CreatedAt)
            .Must(DomainExtension.BeValidPastOrPresentDate)
            .When(c => c.CreatedAt.HasValue)
            .WithErrorCode("InvalidCreatedAt")
            .WithMessage("Creation date cannot be in the future.");
    }
}