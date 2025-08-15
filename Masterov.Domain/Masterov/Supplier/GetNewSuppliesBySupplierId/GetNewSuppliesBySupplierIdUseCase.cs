using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId;
using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId.Query;
using Masterov.Domain.Masterov.Supplier.GetNewSuppliesBySupplierId.Query;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetNewSuppliesBySupplierId;

public class GetNewSuppliesBySupplierIdUseCase(IValidator<GetNewSuppliesBySupplierIdQuery> validator,
    IGetNewSuppliesBySupplierIdStorage storage, IGetSupplierByIdStorage getSupplierByIdStorage) 
    : IGetNewSuppliesBySupplierIdUseCase
{
    public async Task<IEnumerable<SupplyDomain>?> Execute(GetNewSuppliesBySupplierIdQuery getNewSuppliesBySupplierIdQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getNewSuppliesBySupplierIdQuery, cancellationToken);

        var supplierExists = await getSupplierByIdStorage.GetSupplierById(getNewSuppliesBySupplierIdQuery.SupplierId, cancellationToken);
        
        if (supplierExists is null)
            throw new NotFoundByIdException(getNewSuppliesBySupplierIdQuery.SupplierId, "Поставщик");

        return await storage.GetNewSuppliesBySupplierId(getNewSuppliesBySupplierIdQuery.SupplierId, cancellationToken);
    }
}
