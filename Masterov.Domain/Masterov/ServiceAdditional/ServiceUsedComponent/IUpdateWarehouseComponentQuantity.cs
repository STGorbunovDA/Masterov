using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ServiceAdditional.ServiceUsedComponent;

public interface IUpdateWarehouseComponentQuantity
{
    Task<WarehouseDomain> RemoveQuantityWarehouse(Guid warehouseId, int quantityToUse, CancellationToken cancellationToken);
    Task<WarehouseDomain> ReturnQuantityWarehouse(Guid warehouseId, int quantityToUse, CancellationToken cancellationToken);
}