using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByIdWithoutOrders;

public interface IGetFinishedProductByIdWithoutOrdersStorage
{
    Task<FinishedProductWithoutOrdersDomain?> GetFinishedProductByIdWithoutOrders(Guid finishedProductId, CancellationToken cancellationToken);
}