using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsWithoutOrders;

public interface IGetFinishedProductsWithoutOrdersStorage
{
    Task<IEnumerable<FinishedProductWithoutOrdersDomain>> GetFinishedProductsWithoutOrders(CancellationToken cancellationToken);
}