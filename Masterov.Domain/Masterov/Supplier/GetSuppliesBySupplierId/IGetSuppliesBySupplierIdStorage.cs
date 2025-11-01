using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliesBySupplierId;

public interface IGetSuppliesBySupplierIdStorage
{
    Task<IEnumerable<SupplyDomain>?> GetSuppliesBySupplierId(Guid supplierId, CancellationToken cancellationToken);
}