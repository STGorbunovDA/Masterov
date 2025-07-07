using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrderStatus.Command;

public record UpdateProductionOrderStatusCommand(Guid OrderId, ProductionOrderStatus Status);