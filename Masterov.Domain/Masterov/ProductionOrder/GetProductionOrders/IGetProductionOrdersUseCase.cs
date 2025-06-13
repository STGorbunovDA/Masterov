using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrders;

public interface IGetProductionOrdersUseCase
{
    Task<IEnumerable<ProductionOrderDomain>> Execute(CancellationToken cancellationToken);
}