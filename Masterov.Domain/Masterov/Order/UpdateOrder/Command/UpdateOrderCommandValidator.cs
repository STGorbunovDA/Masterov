using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.Order.UpdateOrder.Command;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(q => q.OrderId)
            .NotEmpty()
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be empty.");
        
        RuleFor(q => q.CreatedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt date cannot be in the future.");
        
        RuleFor(q => q.CompletedAt).Cascade(CascadeMode.Stop)
            .LessThanOrEqualTo(DateTime.Now)
            .WithErrorCode("InvalidDate")
            .WithMessage("CompletedAt date cannot be in the future.");
        
        RuleFor(q => q.Status)
            .IsInEnum()
            .WithErrorCode("InvalidStatus")
            .WithMessage("Status must be a valid OrderStatus value.")
            .NotEqual(OrderStatus.Unknown)
            .WithErrorCode("InvalidOrderStatusValue")
            .WithMessage("Status cannot be 'Unknown'.");
        
        RuleFor(q => q.Description)
            .MaximumLength(200)
            .WithErrorCode("DescriptionTooLong")
            .WithMessage("Description cannot exceed 200 characters.")
            .When(q => q.Description != null);
        
        RuleFor(q => q.FinishedProductId)
            .NotEmpty()
            .WithErrorCode("InvalidFinishedProductId")
            .WithMessage("FinishedProductId must not be empty.");
        
        RuleFor(q => q.CustomerId)
            .NotEmpty()
            .WithErrorCode("InvalidCustomerId")
            .WithMessage("CustomerId must not be empty.");
    }
}