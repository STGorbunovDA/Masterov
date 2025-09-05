using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSupplierBySupplyId;

public interface IGetSupplierBySupplyIdStorage
{
    Task<SupplierDomain?> GetSupplierBySupplyId(Guid supplyId, CancellationToken cancellationToken);
}