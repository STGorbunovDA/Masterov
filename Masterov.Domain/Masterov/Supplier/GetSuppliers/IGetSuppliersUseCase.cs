using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliers;

public interface IGetSuppliersUseCase
{
    Task<IEnumerable<SupplierDomain>> Execute(CancellationToken cancellationToken);
}