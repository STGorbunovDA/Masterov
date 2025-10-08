using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductByNameWithoutOrders;

public interface IGetFinishedProductByNameWithoutOrdersStorage
{
    Task<IEnumerable<FinishedProductWithoutOrdersDomain?>> GetFinishedProductByNameWithoutOrders(string finishedProductName, CancellationToken cancellationToken);
}