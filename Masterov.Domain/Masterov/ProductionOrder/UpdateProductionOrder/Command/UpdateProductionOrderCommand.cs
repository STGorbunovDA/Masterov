using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrder.Command;

public record UpdateProductionOrderCommand(
    Guid OrderId,
    DateTime CreatedAt,
    ProductionOrderStatus Status,
    string? Description,
    Guid CustomerId);