using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliers;

public interface IGetSuppliersStorage
{
    Task<IEnumerable<SupplierDomain?>> GetSuppliers(CancellationToken cancellationToken);
}