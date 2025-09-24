using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.UpdateSupply;

public interface IUpdateSupplyStorage
{
    Task<SupplyDomain> UpdateSupply(Guid supplyId, Guid supplierId, Guid productTypeId, Guid warehouseId, int quantity, decimal priceSupply, CancellationToken cancellationToken);
}