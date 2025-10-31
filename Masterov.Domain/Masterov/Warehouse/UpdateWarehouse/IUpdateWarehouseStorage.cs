using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.UpdateWarehouse;

public interface IUpdateWarehouseStorage
{
    Task<WarehouseDomain> UpdateWarehouse(Guid warehouseId, Guid componentTypeId, string name, int quantity, decimal price, CancellationToken cancellationToken);
}