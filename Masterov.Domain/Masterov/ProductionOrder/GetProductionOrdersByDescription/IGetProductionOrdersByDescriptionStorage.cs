using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByDescription;

public interface IGetProductionOrdersByDescriptionStorage
{
    Task<IEnumerable<ProductionOrderDomain>?> GetProductionOrdersByDescription(string description, CancellationToken cancellationToken);
}