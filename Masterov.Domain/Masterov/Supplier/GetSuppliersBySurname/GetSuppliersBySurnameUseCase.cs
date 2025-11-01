using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supplier.GetSuppliersBySurname.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersBySurname;

public class GetSuppliersBySurnameUseCase(IValidator<GetSuppliersBySurnameQuery> validator, IGetSuppliersBySurnameStorage storage) : IGetSuppliersBySurnameUseCase
{
    public async Task<IEnumerable<SupplierDomain?>> Execute(GetSuppliersBySurnameQuery getSuppliersByNameQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSuppliersByNameQuery, cancellationToken);
        var supplierExists = await storage.GetSuppliersBySurname(getSuppliersByNameQuery.SupplierSurname, cancellationToken);
        
        if (supplierExists is null)
            throw new NotFoundByNameException(getSuppliersByNameQuery.SupplierSurname, "Поставщик");
        
        return supplierExists;
    }
}