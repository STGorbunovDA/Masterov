using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.AddProductionOrder;

public interface IAddProductionOrderStorage
{
    Task<ProductionOrderDomain> AddProductionOrder(Guid finishedProductId, Guid customerId, string? description, CancellationToken cancellationToken);
}