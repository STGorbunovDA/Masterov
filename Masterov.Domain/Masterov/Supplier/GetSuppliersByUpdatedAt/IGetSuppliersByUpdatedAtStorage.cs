using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByUpdatedAt;

public interface IGetSuppliersByUpdatedAtStorage
{
    Task<IEnumerable<SupplierDomain>?> GetSuppliersByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken);
}