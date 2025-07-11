using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;

public interface IGetProductionOrderByOrderIdStorage
{
    Task<ProductionOrderDomain?> GetProductionOrderById(Guid orderId, CancellationToken cancellationToken);
}