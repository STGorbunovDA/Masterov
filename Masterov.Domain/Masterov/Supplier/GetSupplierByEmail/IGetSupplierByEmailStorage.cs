using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByEmail;

public interface IGetSupplierByEmailStorage
{
    Task<SupplierDomain?> GetSupplierByEmail(string supplierEmail, CancellationToken cancellationToken);
}