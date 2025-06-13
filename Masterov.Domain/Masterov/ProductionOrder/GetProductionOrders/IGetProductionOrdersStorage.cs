using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrders;

public interface IGetProductionOrdersStorage
{
    Task<IEnumerable<ProductionOrderDomain>> GetProductionOrders(CancellationToken cancellationToken);
}