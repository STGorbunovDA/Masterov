using Masterov.Domain.Masterov.Supplier.GetSuppliersBySurname.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersBySurname;

public interface IGetSuppliersBySurnameUseCase
{
    Task<IEnumerable<SupplierDomain?>> Execute(GetSuppliersBySurnameQuery query, CancellationToken cancellationToken);
}