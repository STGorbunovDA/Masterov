using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus;

public interface IGetProductionOrdersByStatusUseCase
{
    Task<IEnumerable<ProductionOrderDomain>?> Execute(GetProductionOrdersByStatusQuery getProductionOrdersByStatusQuery, CancellationToken cancellationToken);
}