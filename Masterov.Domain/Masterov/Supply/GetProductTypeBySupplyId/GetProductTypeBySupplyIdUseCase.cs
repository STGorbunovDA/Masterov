using FluentValidation;
using Masterov.Domain.Exceptions;
using Masterov.Domain.Masterov.Supply.GetProductTypeBySupplyId.Query;
using Masterov.Domain.Masterov.Supply.GetSupplyById;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetProductTypeBySupplyId;

public class GetProductTypeBySupplyIdUseCase(IValidator<GetProductTypeBySupplyIdQuery> validator, IGetProductTypeBySupplyIdStorage storage, IGetSupplyByIdStorage getSupplyByIdStorage) : IGetProductTypeBySupplyIdUseCase
{
    public async Task<ComponentTypeDomain?> Execute(GetProductTypeBySupplyIdQuery getProductTypeBySupplyIdQuery, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(getProductTypeBySupplyIdQuery, cancellationToken);
        
        var supplyExists = await getSupplyByIdStorage.GetSupplyById(getProductTypeBySupplyIdQuery.SupplyId, cancellationToken);
        
        if (supplyExists is null)
            throw new NotFoundByIdException(getProductTypeBySupplyIdQuery.SupplyId, "Поставка");
        
        return await storage.GetProductTypeBySupplyId(getProductTypeBySupplyIdQuery.SupplyId, cancellationToken);
    }
}