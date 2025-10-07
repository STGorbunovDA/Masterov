using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.Order.UpdateOrderStatus.Command;

public record UpdateOrderStatusCommand(Guid OrderId, OrderStatus Status);