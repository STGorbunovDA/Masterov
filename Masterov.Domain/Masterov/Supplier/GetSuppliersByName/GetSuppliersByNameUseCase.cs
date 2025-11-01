using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByName;

public class GetSuppliersByNameUseCase(IValidator<GetSuppliersByNameQuery> validator, IGetSupplierByNameStorage storage) : IGetSuppliersByNameUseCase
{
    public async Task<IEnumerable<SupplierDomain?>> Execute(GetSuppliersByNameQuery getSuppliersByNameQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSuppliersByNameQuery, cancellationToken);
        var supplierExists = await storage.GetSupplierByName(getSuppliersByNameQuery.SupplierName, cancellationToken);
        
        if (supplierExists is null)
            throw new NotFoundByNameException(getSuppliersByNameQuery.SupplierName, "Поставщик");
        
        return supplierExists;
    }
}