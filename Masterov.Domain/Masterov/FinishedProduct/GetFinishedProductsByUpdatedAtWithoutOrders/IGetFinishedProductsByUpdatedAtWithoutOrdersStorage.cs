using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAtWithoutOrders;

public interface IGetFinishedProductsByUpdatedAtWithoutOrdersStorage
{
    Task<IEnumerable<FinishedProductNoOrdersDomain>?> GetFinishedProductsByUpdatedAtWithoutOrders(DateTime? updatedAt, CancellationToken cancellationToken);
}