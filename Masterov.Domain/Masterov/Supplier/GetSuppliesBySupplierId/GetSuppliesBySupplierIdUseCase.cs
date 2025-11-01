using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supplier.GetSupplierById;
using Masterov.Domain.Masterov.Supplier.GetSuppliesBySupplierId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supplier.GetSuppliesBySupplierId;

public class GetSuppliesBySupplierIdUseCase(IValidator<GetSuppliesBySupplierIdIdQuery> validator,
    IGetSuppliesBySupplierIdStorage storage, IGetSupplierByIdStorage getSupplierByIdStorage) 
    : IGetSuppliesBySupplierIdUseCase
{
    public async Task<IEnumerable<SupplyDomain>?> Execute(GetSuppliesBySupplierIdIdQuery getSuppliesBySupplierIdIdQuery,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSuppliesBySupplierIdIdQuery, cancellationToken);

        var supplierExists = await getSupplierByIdStorage.GetSupplierById(getSuppliesBySupplierIdIdQuery.SupplierId, cancellationToken);
        
        if (supplierExists is null)
            throw new NotFoundByIdException(getSuppliesBySupplierIdIdQuery.SupplierId, "Поставщик");

        return await storage.GetSuppliesBySupplierId(getSuppliesBySupplierIdIdQuery.SupplierId, cancellationToken);
    }
}
