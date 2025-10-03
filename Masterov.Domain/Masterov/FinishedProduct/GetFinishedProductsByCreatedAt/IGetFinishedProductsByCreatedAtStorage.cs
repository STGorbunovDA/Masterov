using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByCreatedAt;

public interface IGetFinishedProductsByCreatedAtStorage
{
    Task<IEnumerable<FinishedProductDomain>?> GetFinishedProductsByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken);
}