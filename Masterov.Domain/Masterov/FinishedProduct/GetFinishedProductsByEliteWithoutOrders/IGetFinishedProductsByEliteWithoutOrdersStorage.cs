using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByEliteWithoutOrders;

public interface IGetFinishedProductsByEliteWithoutOrdersStorage
{
    Task<IEnumerable<FinishedProductNoOrdersDomain?>> GetFinishedProductsByEliteWithoutOrders(CancellationToken cancellationToken);
}