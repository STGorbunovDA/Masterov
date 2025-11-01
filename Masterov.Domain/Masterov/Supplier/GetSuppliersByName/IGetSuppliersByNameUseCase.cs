using Masterov.Domain.Masterov.Supplier.GetSuppliersByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByName;

public interface IGetSuppliersByNameUseCase
{
    Task<IEnumerable<SupplierDomain?>> Execute(GetSuppliersByNameQuery getSuppliersByNameQuery, CancellationToken cancellationToken);
}