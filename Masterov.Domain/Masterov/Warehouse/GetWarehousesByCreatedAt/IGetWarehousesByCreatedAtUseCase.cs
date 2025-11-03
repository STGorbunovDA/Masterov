using Masterov.Domain.Masterov.Warehouse.GetWarehousesByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehousesByCreatedAt;

public interface IGetWarehousesByCreatedAtUseCase
{
    Task<IEnumerable<WarehouseDomain>?> Execute(GetWarehousesByCreatedAtQuery warehousesByCreatedAtQuery, CancellationToken cancellationToken);
}