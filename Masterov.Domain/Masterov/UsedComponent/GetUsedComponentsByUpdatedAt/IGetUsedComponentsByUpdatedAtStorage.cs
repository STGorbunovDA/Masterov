using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByUpdatedAt;

public interface IGetUsedComponentsByUpdatedAtStorage
{
    Task<IEnumerable<UsedComponentDomain>?> GetUsedComponentsByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken);
}