using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByUpdatedAt;

public interface IGetComponentTypesByUpdatedAtStorage
{
    Task<IEnumerable<ComponentTypeDomain>?> GetComponentTypesByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken);
}