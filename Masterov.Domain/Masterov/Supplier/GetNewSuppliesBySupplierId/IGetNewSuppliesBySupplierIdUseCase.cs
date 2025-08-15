using Masterov.Domain.Masterov.Supplier.GetNewSuppliesBySupplierId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetNewSuppliesBySupplierId;

public interface IGetNewSuppliesBySupplierIdUseCase
{
    Task<IEnumerable<SupplyDomain>?> Execute(GetNewSuppliesBySupplierIdQuery getNewSuppliesBySupplierIdQuery, CancellationToken cancellationToken);
}