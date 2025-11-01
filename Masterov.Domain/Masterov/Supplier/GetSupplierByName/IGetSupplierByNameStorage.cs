using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByName;

public interface IGetSupplierByNameStorage
{
    Task<IEnumerable<SupplierDomain?>> GetSupplierByName(string supplierName, CancellationToken cancellationToken);
}