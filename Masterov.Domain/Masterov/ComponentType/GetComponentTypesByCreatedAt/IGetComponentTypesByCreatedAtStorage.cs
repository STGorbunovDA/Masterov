using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByCreatedAt;

public interface IGetComponentTypesByCreatedAtStorage
{
    Task<IEnumerable<ComponentTypeDomain>?> GetComponentTypesByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken);
}