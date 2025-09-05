using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supply.GetSupplierBySupplyId.Query;
using Masterov.Domain.Masterov.Supply.GetSupplyById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSupplierBySupplyId;

public class GetSupplierBySupplyIdUseCase(IValidator<GetSupplierBySupplyIdQuery> validator, IGetSupplierBySupplyIdStorage storage, IGetSupplyByIdStorage getSupplyByIdStorage) : IGetSupplierBySupplyIdUseCase
{
    public async Task<SupplierDomain?> Execute(GetSupplierBySupplyIdQuery getSupplierBySupplyIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getSupplierBySupplyIdQuery, cancellationToken);
        
        var supplyExists = await getSupplyByIdStorage.GetSupplyById(getSupplierBySupplyIdQuery.SupplyId, cancellationToken);
        
        if (supplyExists is null)
            throw new NotFoundByIdException(getSupplierBySupplyIdQuery.SupplyId, "Поставка");
        
        return await storage.GetSupplierBySupplyId(getSupplierBySupplyIdQuery.SupplyId, cancellationToken);
    }
}