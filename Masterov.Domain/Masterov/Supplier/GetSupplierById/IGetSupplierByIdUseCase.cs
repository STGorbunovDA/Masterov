using Masterov.Domain.Masterov.Supplier.GetSupplierById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierById;

public interface IGetSupplierByIdUseCase
{
    Task<SupplierDomain?> Execute(GetSupplierByIdQuery getSupplierByIdQuery, CancellationToken cancellationToken);
}