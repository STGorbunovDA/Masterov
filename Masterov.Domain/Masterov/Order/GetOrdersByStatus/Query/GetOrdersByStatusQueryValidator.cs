using FluentValidation;
using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.Order.GetOrdersByStatus.Query;

public class GetOrdersByStatusQueryValidator : AbstractValidator<GetOrdersByStatusQuery>
{
    public GetOrdersByStatusQueryValidator()
    {
        RuleFor(q => q.Status).Cascade(CascadeMode.Stop)
            .IsInEnum()
            .WithErrorCode("InvalidStatus")
            .WithMessage("Status must be a valid ProductionOrderStatus value.")
            .Must(status => status != OrderStatus.Unknown)
            .WithErrorCode("InvalidStatusValue")
            .WithMessage("Status cannot be 'Unknown'.");
    }
}