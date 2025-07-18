using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierById;

public interface IGetSupplierByIdStorage
{
    Task<SupplierDomain?> GetSupplierById(Guid supplierId, CancellationToken cancellationToken);
}