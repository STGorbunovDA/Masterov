using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypeByName;

public interface IGetComponentTypeByNameStorage
{
    Task<ComponentTypeDomain?> GetComponentTypeByName(string componentTypeName, CancellationToken cancellationToken);
}