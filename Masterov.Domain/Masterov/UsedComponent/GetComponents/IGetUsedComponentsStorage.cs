using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetComponents;

public interface IGetUsedComponentsStorage
{
    Task<IEnumerable<UsedComponentDomain>> GetUsedComponents(CancellationToken cancellationToken);
}