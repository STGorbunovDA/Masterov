using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetNewSuppliesBySupplierId;

public interface IGetNewSuppliesBySupplierIdStorage
{
    Task<IEnumerable<SupplyDomain>?> GetNewSuppliesBySupplierId(Guid supplierId, CancellationToken cancellationToken);
}