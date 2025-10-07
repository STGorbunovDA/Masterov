using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;

public interface IGetProductionOrderByOrderIdStorage
{
    Task<OrderDomain?> GetProductionOrderById(Guid orderId, CancellationToken cancellationToken);
}