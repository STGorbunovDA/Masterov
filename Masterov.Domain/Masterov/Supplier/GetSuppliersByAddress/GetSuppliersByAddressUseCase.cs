using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supplier.GetSuppliersByAddress.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliersByAddress;

public class GetSuppliersByAddressUseCase(IValidator<GetSuppliersByAddressQuery> validator, IGetSupplierByAddressStorage storage) : IGetSuppliersByAddressUseCase
{
    public async Task<IEnumerable<SupplierDomain?>> Execute(GetSuppliersByAddressQuery getSuppliersByAddressQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSuppliersByAddressQuery, cancellationToken);
        var supplierExists = await storage.GetSuppliersByAddress(getSuppliersByAddressQuery.Address, cancellationToken);
        
        if (supplierExists is null)
            throw new NotFoundByAddressException(getSuppliersByAddressQuery.Address);
        
        return supplierExists;
    }
}