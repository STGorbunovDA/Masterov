using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByNameWithoutOrders;

public interface IGetFinishedProductsByNameWithoutOrdersStorage
{
    Task<IEnumerable<FinishedProductNoOrdersDomain?>> GetFinishedProductByNameWithoutOrders(string finishedProductName, CancellationToken cancellationToken);
}