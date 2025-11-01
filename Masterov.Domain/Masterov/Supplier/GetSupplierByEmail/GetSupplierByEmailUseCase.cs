using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supplier.GetSupplierByEmail.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByEmail;

public class GetSupplierByEmailUseCase(IValidator<GetSupplierByEmailQuery> validator, IGetSupplierByEmailStorage storage) : IGetSupplierByEmailUseCase
{
    public async Task<SupplierDomain?> Execute(GetSupplierByEmailQuery supplierByEmailQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(supplierByEmailQuery, cancellationToken);
        var supplierExists = await storage.GetSupplierByEmail(supplierByEmailQuery.Email, cancellationToken);
        
        if (supplierExists is null)
            throw new NotFoundByNameException(supplierByEmailQuery.Email, "Заказчик");
        
        return supplierExists;
    }
}