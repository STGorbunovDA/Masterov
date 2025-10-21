using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByIdWithoutOrders;

public interface IGetFinishedProductByIdWithoutOrdersStorage
{
    Task<FinishedProductNoOrdersDomain?> GetFinishedProductByIdWithoutOrders(Guid finishedProductId, CancellationToken cancellationToken);
}