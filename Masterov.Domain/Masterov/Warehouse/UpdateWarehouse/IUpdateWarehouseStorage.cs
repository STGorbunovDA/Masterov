using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.UpdateWarehouse;

public interface IUpdateWarehouseStorage
{
    Task<WarehouseDomain> UpdateWarehouse(Guid warehouseId, Guid productTypeId, string name, int quantity, decimal price, CancellationToken cancellationToken);
}