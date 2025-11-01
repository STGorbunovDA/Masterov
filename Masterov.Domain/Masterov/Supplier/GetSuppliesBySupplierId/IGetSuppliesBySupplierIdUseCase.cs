using Masterov.Domain.Masterov.Supplier.GetSuppliesBySupplierId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliesBySupplierId;

public interface IGetSuppliesBySupplierIdUseCase
{
    Task<IEnumerable<SupplyDomain>?> Execute(GetSuppliesBySupplierIdIdQuery getSuppliesBySupplierIdIdQuery, CancellationToken cancellationToken);
}