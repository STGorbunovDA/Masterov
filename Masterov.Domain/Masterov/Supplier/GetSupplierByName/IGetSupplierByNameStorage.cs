using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByName;

public interface IGetSupplierByNameStorage
{
    Task<SupplierDomain?> GetSupplierByName(string supplierName, CancellationToken cancellationToken);
}