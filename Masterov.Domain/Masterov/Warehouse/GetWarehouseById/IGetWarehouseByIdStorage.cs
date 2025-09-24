using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehouseById;

public interface IGetWarehouseByIdStorage
{
    Task<WarehouseDomain?> GetWarehouseById(Guid warehouseById, CancellationToken cancellationToken);
}