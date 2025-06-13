using Masterov.Domain.Extension;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus;

public interface IGetProductionOrdersByStatusStorage
{
    Task<IEnumerable<ProductionOrderDomain>?> GetProductionOrdersByStatus(ProductionOrderStatus status, CancellationToken cancellationToken);
}