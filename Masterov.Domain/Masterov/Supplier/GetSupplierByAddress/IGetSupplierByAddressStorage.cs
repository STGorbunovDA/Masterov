using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByAddress;

public interface IGetSupplierByAddressStorage
{
    Task<SupplierDomain?> GetSupplierByAddress(string supplierByAddress, CancellationToken cancellationToken);
}