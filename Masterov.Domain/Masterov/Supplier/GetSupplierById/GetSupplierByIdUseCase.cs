using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supplier.GetSupplierById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierById;

public class GetSupplierByIdUseCase(IValidator<GetSupplierByIdQuery> validator, IGetSupplierByIdStorage storage) : IGetSupplierByIdUseCase
{
    public async Task<SupplierDomain?> Execute(GetSupplierByIdQuery getSupplierByIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSupplierByIdQuery, cancellationToken);
        var supplierExists = await storage.GetSupplierById(getSupplierByIdQuery.SupplierId, cancellationToken);
        
        if (supplierExists is null)
            throw new NotFoundByIdException(getSupplierByIdQuery.SupplierId, "Поставщик");
        
        return supplierExists;
    }
}