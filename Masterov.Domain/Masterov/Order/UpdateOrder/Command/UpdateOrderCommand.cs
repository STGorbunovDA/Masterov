using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.Order.UpdateOrder.Command;

public record UpdateOrderCommand(
    Guid OrderId,
    DateTime CreatedAt,
    DateTime CompletedAt,
    OrderStatus Status,
    string? Description,
    Guid CustomerId);