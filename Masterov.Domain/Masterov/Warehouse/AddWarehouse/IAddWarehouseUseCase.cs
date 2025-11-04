using Masterov.Domain.Masterov.Warehouse.AddWarehouse.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.AddWarehouse;

public interface IAddWarehouseUseCase
{
    Task<WarehouseDomain?> Execute(AddWarehouseCommand addWarehouseCommand, CancellationToken cancellationToken);
}