using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.ServiceUsedComponentAdditional;

public interface IWarehouseService
{
    Task<WarehouseDomain> UpdateQuantityWarehouseAsync(Guid warehouseId, int newQuantity, CancellationToken cancellationToken);
}