using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetWarehouseBySupplyId;

public interface IGetWarehouseBySupplyIdStorage
{
    Task<WarehouseDomain?> GetWarehouseBySupplyId(Guid supplyId, CancellationToken cancellationToken);
}