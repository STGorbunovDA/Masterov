using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;

public interface IGetProductionOrderByIdUseCase
{
    Task<ProductionOrderDomain?> Execute(GetProductionOrderByIdQuery getProductionOrderByIdQuery, CancellationToken cancellationToken);
}