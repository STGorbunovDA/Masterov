using Masterov.Domain.Masterov.UsedComponent.GetWarehouseByUsedComponentId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetWarehouseByUsedComponentId;

public interface IGetWarehouseByUsedComponentIdUseCase
{
    Task<WarehouseDomain?> Execute(GetWarehouseByUsedComponentIdQuery warehouseByUsedComponentIdQuery, CancellationToken cancellationToken);
}