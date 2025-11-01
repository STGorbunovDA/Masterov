using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.UpdatePriceWarehouseById;

public interface IUpdatePriceWarehouseByIdStorage
{
    Task<WarehouseDomain> UpdatePriceWarehouseById(Guid warehouseId, decimal price, CancellationToken cancellationToken);
}