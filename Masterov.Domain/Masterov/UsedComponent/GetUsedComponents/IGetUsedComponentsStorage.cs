using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponents;

public interface IGetUsedComponentsStorage
{
    Task<IEnumerable<UsedComponentDomain?>> GetUsedComponents(CancellationToken cancellationToken);
}