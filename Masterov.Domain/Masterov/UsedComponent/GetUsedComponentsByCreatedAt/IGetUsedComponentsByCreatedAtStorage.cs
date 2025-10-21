using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByCreatedAt;

public interface IGetUsedComponentsByCreatedAtStorage
{
    Task<IEnumerable<UsedComponentDomain>?> GetUsedComponentsByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken);
}