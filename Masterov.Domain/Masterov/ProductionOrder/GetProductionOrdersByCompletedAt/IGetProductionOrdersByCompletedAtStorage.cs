using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCompletedAt;

public interface IGetProductionOrdersByCompletedAtStorage
{
    Task<IEnumerable<ProductionOrderDomain>?> GetProductionOrdersByCompletedAt(DateTime completedAt, CancellationToken cancellationToken);
}