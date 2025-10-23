using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetProductTypeByUsedComponentId;

public interface IGetProductTypeByUsedComponentIdStorage
{
    Task<ProductTypeDomain?> GetProductTypeByUsedComponentId(Guid usedComponentId, CancellationToken cancellationToken);
}