using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByAddress;

public interface IGetSupplierByAddressStorage
{
    Task<IEnumerable<SupplierDomain?>> GetSuppliersByAddress(string supplierAddress, CancellationToken cancellationToken);
}