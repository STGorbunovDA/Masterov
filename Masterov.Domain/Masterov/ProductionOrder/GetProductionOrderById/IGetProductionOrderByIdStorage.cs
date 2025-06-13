using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;

public interface IGetProductionOrderByIdStorage
{
    Task<ProductionOrderDomain?> GetProductionOrderById(Guid orderId, CancellationToken cancellationToken);
}