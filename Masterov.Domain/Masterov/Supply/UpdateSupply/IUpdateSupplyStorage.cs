using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.UpdateSupply;

public interface IUpdateSupplyStorage
{
    Task<SupplyDomain> UpdateSupply(Guid supplyId, Guid supplierId, Guid componentTypeId, Guid warehouseId, int quantity, decimal price, DateTime? createdAt, CancellationToken cancellationToken);
}