using Masterov.Domain.Masterov.Warehouse.GetWarehousesByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehousesByUpdatedAt;

public interface IGetWarehousesByUpdatedAtUseCase
{
    Task<IEnumerable<WarehouseDomain>?> Execute(GetWarehousesByUpdatedAtQuery warehousesByUpdatedAtQuery, CancellationToken cancellationToken);
}