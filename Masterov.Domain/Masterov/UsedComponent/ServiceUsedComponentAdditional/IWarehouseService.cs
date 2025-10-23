using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.ServiceUsedComponentAdditional;

public interface IWarehouseService
{
    Task<WarehouseDomain> RemoveQuantityWarehouse(Guid warehouseId, int quantityToUse, CancellationToken cancellationToken);
    Task<WarehouseDomain> ReturnQuantityWarehouse(Guid warehouseId, int quantityToUse, CancellationToken cancellationToken);
}