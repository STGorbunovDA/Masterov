using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt;

public interface IGetProductionOrdersByCreatedAtStorage
{
    Task<IEnumerable<OrderDomain>?> GetProductionOrdersByCreatedAt(DateTime createdAt, CancellationToken cancellationToken);
}