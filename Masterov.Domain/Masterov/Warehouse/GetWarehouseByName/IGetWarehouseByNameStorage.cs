using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehouseByName;

public interface IGetWarehouseByNameStorage
{
    Task<IEnumerable<WarehouseDomain?>> GetWarehouseByName(string name, CancellationToken cancellationToken);
}