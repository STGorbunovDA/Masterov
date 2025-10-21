using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;

public interface IGetUsedComponentByIdStorage
{
    Task<UsedComponentDomain?> GetUsedComponentById(Guid usedComponentId, CancellationToken cancellationToken);
}