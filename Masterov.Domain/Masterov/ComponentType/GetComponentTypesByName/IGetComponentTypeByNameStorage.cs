using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByName;

public interface IGetComponentTypeByNameStorage
{
    Task<IEnumerable<ComponentTypeDomain?>> GetComponentTypesByName(string componentTypeName, CancellationToken cancellationToken);
}