using FluentValidation;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Supply.UpdateSupply.Command;

namespace Masterov.Domain.Masterov.Warehouse.UpdateWarehouse.Command;

public class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
{
    public UpdateWarehouseCommandValidator()
    {
        RuleFor(q => q.WarehouseId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("WarehouseId must not be an empty GUID.");
        
        RuleFor(q => q.ComponentTypeId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("ComponentTypeId must not be an empty GUID.");
        
        RuleFor(c => c.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithErrorCode("Empty")
            .WithMessage("The name should not be empty.")
            .MaximumLength(100)
            .WithErrorCode("TooLong")
            .WithMessage("The maximum length of the name should not be more than 100");
        
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