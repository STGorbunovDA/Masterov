using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypes;

public interface IGetComponentTypesStorage
{
    Task<IEnumerable<ComponentTypeDomain?>> GetComponentTypes(CancellationToken cancellationToken);
}