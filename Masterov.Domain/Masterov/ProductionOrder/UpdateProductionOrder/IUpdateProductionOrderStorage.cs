using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrder;

public interface IUpdateProductionOrderStorage
{
    Task<ProductionOrderDomain> UpdateProductionOrder(
        Guid orderId, 
        DateTime createdAt, 
        ProductionOrderStatus status,
        string? description, 
        Guid customerId,
        CancellationToken ct);
}