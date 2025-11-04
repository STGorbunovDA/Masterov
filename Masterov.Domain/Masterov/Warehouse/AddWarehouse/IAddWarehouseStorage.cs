using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.AddWarehouse;

public interface IAddWarehouseStorage
{
    Task<WarehouseDomain> AddWarehouse(string name, Guid componentTypeId, CancellationToken cancellationToken);
}