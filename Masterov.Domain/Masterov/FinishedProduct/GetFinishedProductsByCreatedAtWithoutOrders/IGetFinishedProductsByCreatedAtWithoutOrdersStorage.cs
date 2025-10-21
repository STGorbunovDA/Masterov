using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAtWithoutOrders;

public interface IGetFinishedProductsByCreatedAtWithoutOrdersStorage
{
    Task<IEnumerable<FinishedProductNoOrdersDomain>?> GetFinishedProductsByCreatedAtWithoutOrders(DateTime? createdAt, CancellationToken cancellationToken);
}