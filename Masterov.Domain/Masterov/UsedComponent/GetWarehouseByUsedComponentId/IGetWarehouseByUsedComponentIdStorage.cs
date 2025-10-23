using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetWarehouseByUsedComponentId;

public interface IGetWarehouseByUsedComponentIdStorage
{
    Task<WarehouseDomain?> GetWarehouseByUsedComponentId(Guid usedComponentId, CancellationToken cancellationToken);
}