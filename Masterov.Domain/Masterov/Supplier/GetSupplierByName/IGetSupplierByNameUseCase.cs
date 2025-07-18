using Masterov.Domain.Masterov.Supplier.GetSupplierByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByName;

public interface IGetSupplierByNameUseCase
{
    Task<SupplierDomain?> Execute(GetSupplierByNameQuery getSupplierByNameQuery, CancellationToken cancellationToken);
}