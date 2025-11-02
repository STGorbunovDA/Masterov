using Masterov.Domain.Masterov.Supplier.GetSuppliersByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByUpdatedAt;

public interface IGetSuppliersByUpdatedAtUseCase
{
    Task<IEnumerable<SupplierDomain>?> Execute(GetSuppliersByUpdatedAtQuery suppliersByUpdatedAtQuery, CancellationToken cancellationToken);
}