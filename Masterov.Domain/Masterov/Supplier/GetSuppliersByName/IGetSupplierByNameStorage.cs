using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByName;

public interface IGetSupplierByNameStorage
{
    Task<IEnumerable<SupplierDomain?>> GetSupplierByName(string supplierName, CancellationToken cancellationToken);
}