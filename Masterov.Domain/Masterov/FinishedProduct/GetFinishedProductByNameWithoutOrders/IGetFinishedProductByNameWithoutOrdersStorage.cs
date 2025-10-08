using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByNameWithoutOrders;

public interface IGetFinishedProductByNameWithoutOrdersStorage
{
    Task<FinishedProductWithoutOrdersDomain?> GetFinishedProductByNameWithoutOrders(string finishedProductName, CancellationToken cancellationToken);
}