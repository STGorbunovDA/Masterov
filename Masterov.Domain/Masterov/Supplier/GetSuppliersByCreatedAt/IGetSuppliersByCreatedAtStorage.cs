using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByCreatedAt;

public interface IGetSuppliersByCreatedAtStorage
{
    Task<IEnumerable<SupplierDomain>?> GetSuppliersByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken);
}