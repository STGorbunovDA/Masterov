using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetUsedComponentsByComponentTypeId;

public interface IGetUsedComponentsByComponentTypeIdStorage
{
    Task<IEnumerable<UsedComponentDomain>?> GetUsedComponentsByComponentTypeId(Guid componentTypeId, CancellationToken cancellationToken);
}