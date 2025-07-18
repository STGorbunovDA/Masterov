using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrder.Command;

public class UpdateProductionOrderCommandValidator : AbstractValidator<UpdateProductionOrderCommand>
{
    public UpdateProductionOrderCommandValidator()
    {
        RuleFor(q => q.OrderId)
            .NotEmpty()
            .WithErrorCode("InvalidId")
            .WithMessage("OrderId must not be empty.");

        RuleFor(q => q.CreatedAt)
            .NotEmpty()
            .WithErrorCode("InvalidDate")
            .WithMessage("CreatedAt must be specified.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithErrorCode("FutureDate")
            .WithMessage("CreatedAt cannot be in the future.");

        RuleFor(q => q.Status)
            .IsInEnum()
            .WithErrorCode("InvalidStatus")
            .WithMessage("Status must be a valid ProductionOrderStatus value.")
            .NotEqual(ProductionOrderStatus.Unknown)
            .WithErrorCode("InvalidStatusValue")
            .WithMessage("Status cannot be 'Unknown'.");

        RuleFor(q => q.Description)
            .MaximumLength(200)
            .WithErrorCode("DescriptionTooLong")
            .WithMessage("Description cannot exceed 200 characters.")
            .When(q => q.Description != null);

        RuleFor(q => q.CustomerId)
            .NotEmpty()
            .WithErrorCode("InvalidCustomerId")
            .WithMessage("CustomerId must not be empty.");
    }
}