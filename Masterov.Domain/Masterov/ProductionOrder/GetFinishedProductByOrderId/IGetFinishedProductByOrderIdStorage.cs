using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductByOrderId;

public interface IGetFinishedProductByOrderIdStorage
{
    Task<FinishedProductDomain?> GetFinishedProductByOrderId(Guid orderId, CancellationToken cancellationToken);
}