using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.UpdateUsedComponent;

public interface IUpdateUsedComponentStorage
{
    Task<UsedComponentDomain> UpdateUsedComponent(Guid usedComponentId, Guid orderId, Guid componentTypeId, Guid warehouseId,
        int quantity, DateTime? createdAt, CancellationToken cancellationToken);
}