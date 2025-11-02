using FluentValidation;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByCreatedAt;

public class GetSuppliersByCreatedAtUseCase(IValidator<GetSuppliersByCreatedAtQuery> validator,
    IGetSuppliersByCreatedAtStorage storage) 
    : IGetSuppliersByCreatedAtUseCase
{
    public async Task<IEnumerable<SupplierDomain>?> Execute(GetSuppliersByCreatedAtQuery suppliersByCreatedAtQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(suppliersByCreatedAtQuery, cancellationToken);
        return await storage.GetSuppliersByCreatedAt(suppliersByCreatedAtQuery.CreatedAt, cancellationToken);
    }
}