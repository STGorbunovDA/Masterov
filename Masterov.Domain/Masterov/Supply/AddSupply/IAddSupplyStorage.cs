using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.AddSupply;

public interface IAddSupplyStorage
{
    Task<SupplyDomain> AddSupply(Guid supplierId, Guid componentTypeId, Guid warehouseId, int quantity, decimal priceSupply, CancellationToken cancellationToken);
}