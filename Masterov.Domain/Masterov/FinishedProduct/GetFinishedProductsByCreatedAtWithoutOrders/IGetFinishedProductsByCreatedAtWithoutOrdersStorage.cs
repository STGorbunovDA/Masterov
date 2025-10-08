using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAtWithoutOrders;

public interface IGetFinishedProductsByCreatedAtWithoutOrdersStorage
{
    Task<IEnumerable<FinishedProductWithoutOrdersDomain>?> GetFinishedProductsByCreatedAtWithoutOrders(DateTime? createdAt, CancellationToken cancellationToken);
}