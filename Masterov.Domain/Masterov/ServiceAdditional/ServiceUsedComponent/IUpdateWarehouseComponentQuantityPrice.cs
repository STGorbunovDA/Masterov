using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ServiceAdditional.ServiceUsedComponent;

public interface IUpdateWarehouseComponentQuantityPrice
{
    Task<WarehouseDomain> RemoveQuantityPriceWarehouse(Guid warehouseId, int quantityToUse, CancellationToken cancellationToken);
    Task<WarehouseDomain> ReturnQuantityPriceWarehouse(Guid warehouseId, int quantityToUse, CancellationToken cancellationToken);
}