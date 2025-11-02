using FluentValidation;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByUpdatedAt;

public class GetSuppliersByUpdatedAtUseCase(IValidator<GetSuppliersByUpdatedAtQuery> validator,
    IGetSuppliersByUpdatedAtStorage storage) 
    : IGetSuppliersByUpdatedAtUseCase
{
    public async Task<IEnumerable<SupplierDomain>?> Execute(GetSuppliersByUpdatedAtQuery suppliersByUpdatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(suppliersByUpdatedAtQuery, cancellationToken);
        return await storage.GetSuppliersByUpdatedAt(suppliersByUpdatedAtQuery.UpdatedAt, cancellationToken);
    }
}