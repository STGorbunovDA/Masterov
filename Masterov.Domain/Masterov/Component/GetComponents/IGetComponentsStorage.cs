using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Component.GetComponents;

public interface IGetComponentsStorage
{
    Task<IEnumerable<ComponentsDomain>> GetComponents(CancellationToken cancellationToken);
}