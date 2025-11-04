using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypeByWarehouseId;

public interface IGetComponentTypeByWarehouseIdStorage
{
    Task<ComponentTypeDomain?> GetComponentTypeByWarehouseId(Guid warehouseId, CancellationToken cancellationToken);
}