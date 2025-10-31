using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetComponentTypeByUsedComponentId;

public interface IGetComponentTypeByUsedComponentIdStorage
{
    Task<ComponentTypeDomain?> GetComponentTypeByUsedComponentId(Guid usedComponentId, CancellationToken cancellationToken);
}