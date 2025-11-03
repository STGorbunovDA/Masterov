using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehousesByCreatedAt;

public interface IGetWarehousesByCreatedAtStorage
{
    Task<IEnumerable<WarehouseDomain>?> GetWarehousesByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken);
}