using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetSuppliesByWarehouseId;

public interface IGetSuppliesByWarehouseIdStorage
{
    Task<IEnumerable<SupplyDomain>?> GetSuppliesByWarehouseId(Guid warehouseId, CancellationToken cancellationToken);
}