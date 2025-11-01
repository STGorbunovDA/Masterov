using Masterov.Domain.Masterov.Supplier.GetSupplierByEmail.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByEmail;

public interface IGetSupplierByEmailUseCase
{
    Task<SupplierDomain?> Execute(GetSupplierByEmailQuery supplierByEmailQuery, CancellationToken cancellationToken);
}