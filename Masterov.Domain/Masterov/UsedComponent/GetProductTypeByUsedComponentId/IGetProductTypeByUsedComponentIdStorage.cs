using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetProductTypeByUsedComponentId;

public interface IGetProductTypeByUsedComponentIdStorage
{
    Task<ComponentTypeDomain?> GetProductTypeByUsedComponentId(Guid usedComponentId, CancellationToken cancellationToken);
}