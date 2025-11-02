using Masterov.Domain.Masterov.Supplier.GetSuppliersByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByCreatedAt;

public interface IGetSuppliersByCreatedAtUseCase
{
    Task<IEnumerable<SupplierDomain>?> Execute(GetSuppliersByCreatedAtQuery suppliersByCreatedAtQuery, CancellationToken cancellationToken);
}