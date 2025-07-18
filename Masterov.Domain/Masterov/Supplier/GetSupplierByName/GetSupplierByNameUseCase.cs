using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supplier.GetSupplierByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByName;

public class GetSupplierByNameUseCase(IValidator<GetSupplierByNameQuery> validator, IGetSupplierByNameStorage storage) : IGetSupplierByNameUseCase
{
    public async Task<SupplierDomain?> Execute(GetSupplierByNameQuery getSupplierByNameQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSupplierByNameQuery, cancellationToken);
        var supplierExists = await storage.GetSupplierByName(getSupplierByNameQuery.SupplierName, cancellationToken);
        
        if (supplierExists is null)
            throw new NotFoundByNameException(getSupplierByNameQuery.SupplierName, "Поставщик");
        
        return supplierExists;
    }
}