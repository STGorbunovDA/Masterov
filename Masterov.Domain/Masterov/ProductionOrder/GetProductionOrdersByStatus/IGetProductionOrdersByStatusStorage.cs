using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus;

public interface IGetProductionOrdersByStatusStorage
{
    Task<IEnumerable<OrderDomain>?> GetProductionOrdersByStatus(ProductionOrderStatus status, CancellationToken cancellationToken);
}