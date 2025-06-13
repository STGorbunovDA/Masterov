using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt;

public interface IGetProductionOrdersByCreatedAtUseCase
{
    Task<IEnumerable<ProductionOrderDomain>?> Execute(GetProductionOrdersByCreatedAtQuery getProductionOrdersByCreatedAtQuery, CancellationToken cancellationToken);
}