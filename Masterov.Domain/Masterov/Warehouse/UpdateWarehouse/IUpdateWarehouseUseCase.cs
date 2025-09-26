using Masterov.Domain.Masterov.Warehouse.UpdateWarehouse.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.UpdateWarehouse;

public interface IUpdateWarehouseUseCase
{
    Task<WarehouseDomain> Execute(UpdateWarehouseCommand command, CancellationToken cancellationToken);
}