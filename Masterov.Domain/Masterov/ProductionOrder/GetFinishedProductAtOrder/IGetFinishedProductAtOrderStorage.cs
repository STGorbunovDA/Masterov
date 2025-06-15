using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder;

public interface IGetFinishedProductAtOrderStorage
{
    Task<FinishedProductDomain?> GetFinishedProductAtOrder(Guid orderId, CancellationToken cancellationToken);
}