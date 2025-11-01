using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByDescription;

public interface IGetComponentTypesByDescriptionStorage
{
    Task<IEnumerable<ComponentTypeDomain>?> GetComponentTypesByDescription(string description, CancellationToken cancellationToken);
}