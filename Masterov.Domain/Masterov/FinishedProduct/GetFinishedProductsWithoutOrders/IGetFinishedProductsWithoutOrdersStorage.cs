using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsWithoutOrders;

public interface IGetFinishedProductsWithoutOrdersStorage
{
    Task<IEnumerable<FinishedProductNoOrdersDomain>> GetFinishedProductsWithoutOrders(CancellationToken cancellationToken);
}