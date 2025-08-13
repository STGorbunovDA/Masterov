using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supplier.GetSupplierByPhone.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSupplierByPhone;

public class GetSupplierByPhoneUseCase(IValidator<GetSupplierByPhoneQuery> validator, IGetSupplierByPhoneStorage storage) : IGetSupplierByPhoneUseCase
{
    public async Task<SupplierDomain?> Execute(GetSupplierByPhoneQuery getSupplierByPhoneQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSupplierByPhoneQuery, cancellationToken);
        var supplierExists = await storage.GetSupplierByPhone(getSupplierByPhoneQuery.Phone, cancellationToken);
        
        if (supplierExists is null)
            throw new NotFoundByPhoneException(getSupplierByPhoneQuery.Phone);
        
        return supplierExists;
    }
}