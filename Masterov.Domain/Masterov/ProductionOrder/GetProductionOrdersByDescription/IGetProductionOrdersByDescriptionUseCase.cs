using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByDescription.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByDescription;

public interface IGetProductionOrdersByDescriptionUseCase
{
    Task<IEnumerable<ProductionOrderDomain>?> Execute(GetProductionOrdersByDescriptionQuery getProductionOrdersByDescriptionQuery, CancellationToken cancellationToken);
}