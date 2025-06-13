using Masterov.Domain.Extension;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus.Query;

public record GetProductionOrdersByStatusQuery(ProductionOrderStatus Status);