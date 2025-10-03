using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByUpdatedAt;

public interface IGetFinishedProductsByUpdatedAtStorage
{
    Task<IEnumerable<FinishedProductDomain>?> GetFinishedProductsByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken);
}