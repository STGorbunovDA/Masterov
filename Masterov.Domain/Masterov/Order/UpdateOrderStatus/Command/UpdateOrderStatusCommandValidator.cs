using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.Order.UpdateOrderStatus.Command;

public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusCommandValidator()
    {
        RuleFor(q => q.OrderId).Cascade(CascadeMode.Stop)
            .NotEqual(Guid.Empty)
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be an empty GUID.");
        
        RuleFor(q => q.Status).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithErrorCode("InvalidStatus")
            .WithMessage("Status must be a valid ProductionOrderStatus value.")
            .Must(status => status != OrderStatus.Unknown)
            .WithErrorCode("InvalidStatusValue")
            .WithMessage("Status cannot be 'Unknown'.");
        
    }
}