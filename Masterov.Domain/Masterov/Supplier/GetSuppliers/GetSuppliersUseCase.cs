using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliers;

public class GetSuppliersUseCase(IGetSuppliersStorage storage) : IGetSuppliersUseCase
{
    public async Task<IEnumerable<SupplierDomain?>> Execute(CancellationToken cancellationToken)
    {
        return await storage.GetSuppliers(cancellationToken);
    }
      
}