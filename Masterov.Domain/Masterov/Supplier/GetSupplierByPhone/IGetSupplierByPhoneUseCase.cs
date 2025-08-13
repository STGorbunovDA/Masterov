using Masterov.Domain.Masterov.Supplier.GetSupplierByPhone.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByPhone;

public interface IGetSupplierByPhoneUseCase
{
    Task<SupplierDomain?> Execute(GetSupplierByPhoneQuery getSupplierByPhoneQuery, CancellationToken cancellationToken);
}