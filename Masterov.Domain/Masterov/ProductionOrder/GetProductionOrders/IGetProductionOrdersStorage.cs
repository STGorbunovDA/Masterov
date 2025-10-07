using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrders;

public interface IGetProductionOrdersStorage
{
    Task<IEnumerable<OrderDomain>> GetProductionOrders(CancellationToken cancellationToken);
}