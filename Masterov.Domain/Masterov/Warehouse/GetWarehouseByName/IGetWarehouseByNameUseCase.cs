using Masterov.Domain.Masterov.Warehouse.GetWarehouseByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehouseByName;

public interface IGetWarehouseByNameUseCase
{
    Task<IEnumerable<WarehouseDomain?>> Execute(GetWarehouseByNameQuery warehouseByNameQuery, CancellationToken cancellationToken);
}