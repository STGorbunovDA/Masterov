using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByPhone;

public interface IGetSupplierByPhoneStorage
{
    Task<SupplierDomain?> GetSupplierByPhone(string supplierByPhone, CancellationToken cancellationToken);
}