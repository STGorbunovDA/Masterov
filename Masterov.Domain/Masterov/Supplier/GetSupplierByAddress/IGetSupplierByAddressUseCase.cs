using Masterov.Domain.Masterov.Supplier.GetSupplierByAddress.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByAddress;

public interface IGetSupplierByAddressUseCase
{
    Task<SupplierDomain?> Execute(GetSupplierByAddressQuery getSupplierByAddressQuery, CancellationToken cancellationToken);
}