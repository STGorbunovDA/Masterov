using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersBySurname;

public interface IGetSuppliersBySurnameStorage
{
    Task<IEnumerable<SupplierDomain?>> GetSuppliersBySurname(string supplierSurname, CancellationToken cancellationToken);
}