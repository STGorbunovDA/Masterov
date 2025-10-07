using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.Order.UpdateOrder.Command;

public record UpdateOrderCommand(
    Guid OrderId,
    DateTime CreatedAt,
    OrderStatus Status,
    string? Description,
    Guid CustomerId);