using Masterov.Domain.Masterov.Warehouse.GetSuppliesByWarehouseId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Warehouse.GetSuppliesByWarehouseId;

public interface IGetSuppliesByWarehouseIdUseCase
{
    Task<IEnumerable<SupplyDomain>?> Execute(GetSuppliesByWarehouseIdQuery suppliesByWarehouseIdQuery, CancellationToken cancellationToken);
}