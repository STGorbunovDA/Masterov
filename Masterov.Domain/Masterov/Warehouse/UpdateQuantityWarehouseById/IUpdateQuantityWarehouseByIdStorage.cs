using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById;

public interface IUpdateQuantityWarehouseByIdStorage
{
    Task<WarehouseDomain> UpdateQuantityWarehouseById(Guid warehouseId, int quantity, CancellationToken cancellationToken);
}