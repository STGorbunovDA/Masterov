using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus;

public interface IGetProductionOrdersByStatusUseCase
{
    Task<IEnumerable<OrderDomain>?> Execute(GetProductionOrdersByStatusQuery getProductionOrdersByStatusQuery, CancellationToken cancellationToken);
}