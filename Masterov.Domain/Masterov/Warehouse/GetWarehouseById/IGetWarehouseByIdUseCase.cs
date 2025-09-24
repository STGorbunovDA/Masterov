using Masterov.Domain.Masterov.Warehouse.GetWarehouseById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetWarehouseById;

public interface IGetWarehouseByIdUseCase
{
    Task<WarehouseDomain?> Execute(GetWarehouseByIdQuery warehouseByIdQuery, CancellationToken cancellationToken);
}