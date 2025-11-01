using Masterov.Domain.Masterov.Supplier.GetSuppliersByAddress.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByAddress;

public interface IGetSuppliersByAddressUseCase
{
    Task<IEnumerable<SupplierDomain?>> Execute(GetSuppliersByAddressQuery getSuppliersByAddressQuery, CancellationToken cancellationToken);
}