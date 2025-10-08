using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAtWithoutOrders;

public interface IGetFinishedProductsByUpdatedAtWithoutOrdersStorage
{
    Task<IEnumerable<FinishedProductWithoutOrdersDomain>?> GetFinishedProductsByUpdatedAtWithoutOrders(DateTime? updatedAt, CancellationToken cancellationToken);
}