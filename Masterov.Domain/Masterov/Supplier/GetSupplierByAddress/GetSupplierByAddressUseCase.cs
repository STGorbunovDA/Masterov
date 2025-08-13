using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supplier.GetSupplierByAddress.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByAddress;

public class GetSupplierByAddressUseCase(IValidator<GetSupplierByAddressQuery> validator, IGetSupplierByAddressStorage storage) : IGetSupplierByAddressUseCase
{
    public async Task<SupplierDomain?> Execute(GetSupplierByAddressQuery getSupplierByAddressQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSupplierByAddressQuery, cancellationToken);
        var supplierExists = await storage.GetSupplierByAddress(getSupplierByAddressQuery.Address, cancellationToken);
        
        if (supplierExists is null)
            throw new NotFoundByAddressException(getSupplierByAddressQuery.Address);
        
        return supplierExists;
    }
}