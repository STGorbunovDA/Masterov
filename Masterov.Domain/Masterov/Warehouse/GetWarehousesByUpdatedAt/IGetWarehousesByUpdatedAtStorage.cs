using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehousesByUpdatedAt;

public interface IGetWarehousesByUpdatedAtStorage
{
    Task<IEnumerable<WarehouseDomain>?> GetWarehousesByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken);
}